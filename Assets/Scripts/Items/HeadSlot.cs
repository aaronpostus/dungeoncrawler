using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadSlot : MonoBehaviour
{

    [SerializeField] public Image itemImage;

    public void EquipHeadSlot(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }
}

