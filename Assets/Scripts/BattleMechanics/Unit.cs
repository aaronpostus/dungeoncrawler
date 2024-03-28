using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel, damage, maxHP, currentHP, defense;
    public Slider slider;
    public string enemyType;
    public bool TakeDamage(float dmg)
    {
        currentHP -= ((int)dmg - defense);
        //slider.value = currentHP;
        if (currentHP <= 0)
        {
            return true;
        }
        return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

}
