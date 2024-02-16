using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public string name;
    public int count;

    public Item(string itemName, int itemCount)
    {
        name = itemName;
        count = itemCount;
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
