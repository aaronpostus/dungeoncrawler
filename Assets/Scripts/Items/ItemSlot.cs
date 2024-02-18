using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    //======ITEM DATA======//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    //======ITEM SLOT======//
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    //======ITEM DESCRIPTION======//
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionName;
    public TMP_Text ItemDescriptionText;
    public Sprite emptySprite;

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string description)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = description;
        isFull = true;

        quantityText.enabled = true;
        quantityText.text = quantity.ToString();
        itemImage.sprite = itemSprite;
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

        if (itemDescriptionImage.sprite == null) 
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }
}
