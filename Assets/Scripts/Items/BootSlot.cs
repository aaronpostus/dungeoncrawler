using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootSlot : MonoBehaviour
{

    [SerializeField] public Image itemImage;

    public void EquipBootSlot(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }
}
