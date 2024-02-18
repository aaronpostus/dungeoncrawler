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
    private PlayerInput playerInput;
    private InputAction pickup;
    [SerializeField] private GameObject pickupPopup;

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
        Inventory.instance.AddItem(this);
        Destroy(gameObject);
        pickupPopup.SetActive(false);
    }

    public Item(string itemName, string itemType)
    {
        name = itemName;
        type = itemType;
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
}
