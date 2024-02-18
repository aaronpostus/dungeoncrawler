using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.InputSystem;
using Unity.VisualScripting.Antlr3.Runtime;

public class Item : MonoBehaviour
{
    public string name;
    public string type;
    public Sprite typeSprite;
    public Sprite sprite;
    [TextArea] public string description;

    private PlayerInput playerInput;
    private InputAction pickup;
    [SerializeField] private GameObject pickupPopup;

    //=====EQUIP ITEM SLOTS=====//
    [SerializeField] private WeaponSlot weapon;
    [SerializeField] private ArmorSlot armor;
    [SerializeField] private BootSlot boot;
    [SerializeField] private HeadSlot head;

    public Item(string itemName, string itemType, Sprite itemSprite)
    {
        name = itemName;
        type = itemType;
        sprite = itemSprite;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnDisable()
    {
        pickup.Disable();
    }
    public void Pickup(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0f)
        {
            Inventory.instance.AddItem(this);
            Destroy(gameObject);
            pickupPopup.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        pickup = playerInput.Gameplay.PickUp;
        pickup.Enable();

        if (other.gameObject.name == "Check the floor")
        {
            pickup.performed += Pickup;
            pickupPopup.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        pickup.Disable();
        pickupPopup.SetActive(false);
    }

    public void Equip()
    {
        switch (type)
        {
            case "Weapon":
                weapon.EquipWeaponSlot(sprite);
                break;
            case "Armor":
                armor.EquipArmorSlot(sprite);
                break;
            case "Boots":
                boot.EquipBootSlot(sprite);
                break;
            case "Head":
                head.EquipHeadSlot(sprite);
                break;
            default:
                break;
        }
    }
}
