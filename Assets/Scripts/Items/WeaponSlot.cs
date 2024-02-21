using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{

    [SerializeField] public Image itemImage;
    [SerializeField] public Sprite emptyImage;

    public void EquipWeaponSlot(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }

    public void UnEquipWeaponSlot(GameObject playerWeapon)
    {
        itemImage.sprite = emptyImage;
        playerWeapon.SetActive(false);
    }
}

