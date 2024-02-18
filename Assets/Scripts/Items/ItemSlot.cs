using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemSlot : MonoBehaviour
{
    //======ITEM DATA======//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    //======ITEM SLOT======//
    [SerializeField] public TMP_Text quantityText;
    [SerializeField] public Image itemImage;

    //======ITEM DESCRIPTION======//
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionName;
    public TMP_Text ItemDescriptionText;
    public Image itemDescriptionType;

    public Sprite emptySprite;
    public Sprite itemType;

    //======EQUIP ITEM======//
    private PlayerInput playerInput;
    private InputAction equip;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, Sprite itemType, string description)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = description;
        this.itemType = itemType;
        isFull = true;

        quantityText.text = quantity.ToString();
        itemImage.sprite = itemSprite;
    }

    public void RemoveItem()
    {
        this.itemName = "";
        this.quantity = 0;
        this.itemSprite = emptySprite;
        this.itemDescription = "";
        isFull = false;

        quantityText.text = quantity.ToString();
        itemImage.sprite = itemSprite;
        ItemDescriptionName.text = "";
        ItemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
        itemDescriptionType.sprite = emptySprite;
    }

    public void UpdateCount(int newCount)
    {
        quantityText.text = newCount.ToString();
    }

    public void OnItemSelect()
    {
        ItemDescriptionName.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        itemDescriptionType.sprite = itemType;

        if (itemDescriptionImage.sprite != null)
        {
            equip = playerInput.Gameplay.EquipItem;
            equip.Enable();
            equip.performed += EquipItem;
        }
        
        if (itemDescriptionImage.sprite == null) 
        {
            itemDescriptionImage.sprite = emptySprite;
            if (equip != null)
            {
                equip.Disable();
            }
        }
    }

    public void EquipItem(InputAction.CallbackContext context)
    {
        if (itemName != "")
        {
            Inventory.instance.EquipItem(itemName);
            Inventory.instance.RemoveItem(itemName);
        }
    }
}
