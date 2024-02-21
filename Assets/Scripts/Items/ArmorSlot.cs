using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlot : MonoBehaviour
{

    [SerializeField] public Image itemImage;

    public void EquipArmorSlot(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }
}
