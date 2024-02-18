using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Dictionary<Item, int> items = new Dictionary<Item, int>();
    public ItemSlot[] itemSlots;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        bool itemExists = false;
        int count = 1;
        
        foreach (KeyValuePair<Item, int> item in items)
        {
            if (item.Key.name == itemToAdd.name)
            {
                Item temp = item.Key;
                count = item.Value + 1;
                items.Remove(item.Key);
                items.Add(temp, count);
                itemExists = true;
                break;
            }
        }
        if (!itemExists)
        {
            items.Add(itemToAdd, 1);
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemToAdd.name == itemSlots[i].itemName) 
            {
                itemSlots[i].UpdateCount(count);
                break;
            } else if (itemSlots[i].isFull == false)
            {
                itemSlots[i].AddItem(itemToAdd.name, count, itemToAdd.sprite, itemToAdd.typeSprite, itemToAdd.description);
                break;
            }
        }
    }

    public void RemoveItem(string name)
    {
        int count = 1;

        foreach (KeyValuePair<Item, int> item in items)
        {
            if (item.Key.name == name)
            {
                Item temp = item.Key;
                count = item.Value - 1;
                items.Remove(item.Key);
                items.Add(temp, count);
                if (item.Value <= 0)
                {
                    items.Remove(item.Key);
                }
                break;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (name == itemSlots[i].itemName)
            {
                itemSlots[i].UpdateCount(count);

                if (count <= 0)
                {
                    itemSlots[i].RemoveItem();
                }
                break;
            }
        }
    }

    public void EquipItem(string name)
    {
        foreach (KeyValuePair<Item, int> item in items)
        {
            if (item.Key.name == name)
            {
                item.Key.Equip();
            }
        }
    }
}
