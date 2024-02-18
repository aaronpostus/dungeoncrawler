using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{

    [SerializeField] public Image itemImage;

    public void EquipWeaponSlot(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }
}

