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
                return;
            } else if (itemSlots[i].isFull == false)
            {
                itemSlots[i].AddItem(itemToAdd.name, count, itemToAdd.sprite, itemToAdd.description);
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        foreach (KeyValuePair<Item, int> item in items)
        {
            if (item.Key.name == itemToRemove.name)
            {
                Item temp = item.Key;
                int count = item.Value - 1;
                items.Remove(item.Key);
                items.Add(temp, count);
                if (item.Value <= 0)
                {
                    items.Remove(itemToRemove);
                }
                break;
            }
        }
    }
}
