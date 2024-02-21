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
    [SerializeField] private GameObject playerWeapon;


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
            Inventory.instance.pickupPopup.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        pickup = playerInput.Gameplay.PickUp;
        pickup.Enable();

        Debug.Log(other.gameObject.name);


        if (other.gameObject.name == "Check the floor")
        {
            Debug.Log(this.gameObject.name);
            pickup.performed += Pickup;
            Inventory.instance.pickupPopup.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        pickup.Disable();
        Inventory.instance.pickupPopup.SetActive(false);
    }

    public void Equip()
    {
        switch (type)
        {
            case "Weapon":
                playerWeapon.SetActive(true);
                Inventory.instance.weapon.EquipWeaponSlot(sprite);
                break;
            case "Armor":
                Inventory.instance.armor.EquipArmorSlot(sprite);
                break;
            case "Boots":
                Inventory.instance.boot.EquipBootSlot(sprite);
                break;
            case "Head":
                Inventory.instance.head.EquipHeadSlot(sprite);
                break;
            default:
                break;
        }
    }
}
