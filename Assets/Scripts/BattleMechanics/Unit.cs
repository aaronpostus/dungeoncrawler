using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Status { HEALTHY, POISON, PARALYSIS, BURN, SLEEP }

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel, damage, maxHP, currentHP, defense;
    public Slider slider;
    public Status currentStatus = Status.HEALTHY;
    public int turnCount;

    public bool TakeDamage(float dmg)
    {
        currentHP -= ((int)dmg - defense);
        currentHP -= DoT(currentStatus);
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

    public void inflictStatus(Status status)
    {
        if (currentStatus == Status.HEALTHY)
        {
            currentStatus = status;
            turnCount = 0;
        }
    }

    public void incTurnCount()
    {
        if (currentStatus != Status.HEALTHY)
        {
            turnCount++;
            if (turnCount > 3 && currentStatus == Status.SLEEP)
            {
                currentStatus = Status.HEALTHY;
            }
        }
    }

    public int DoT(Status status)
    {
        float dmg = (float)currentHP/8;
        return (int)dmg;
    }

}
