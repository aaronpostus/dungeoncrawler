using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        bool itemExists = false;

        foreach (Item item in items)
        {
            if (item.name == itemToAdd.name)
            {
                item.count += itemToAdd.count;
                itemExists = true;
                break;
            }
        }
        if (!itemExists)
        {
            items.Add(itemToAdd);
        }
        Debug.Log(items.ToString());
    }
}
