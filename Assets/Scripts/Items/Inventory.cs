using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Dictionary<Item, int> items = new Dictionary<Item, int>();

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

        foreach (KeyValuePair<Item, int> item in items)
        {
            if (item.Key.name == itemToAdd.name)
            {
                Item temp = item.Key;
                int count = item.Value + 1;
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
        Debug.Log("count: "+ items.Count);
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
