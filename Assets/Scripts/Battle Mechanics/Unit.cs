using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel, damage, maxHP, currentHP, defense;

    public bool TakeDamage(float dmg)
    {
        currentHP -= ((int) dmg - defense);
        if (currentHP <= 0) {
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
