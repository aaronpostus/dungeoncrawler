using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public string name;
    public string type;

    public Item(string itemName, string itemType)
    {
        name = itemName;
        type = itemType;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inventory.instance.AddItem(this);
            Destroy(gameObject);
        }
    }
}
