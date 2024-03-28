using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, ISaveData
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject circuitBoxTrigger;
    [SerializeField] GameObject electricity;
    public void LoadData(GameData data)
    {
        if (data.floorElevatorOpened) {
            anim.SetBool("Open", true);
            circuitBoxTrigger.SetActive(false);
            Cursor.visible = false;
            electricity.SetActive(true);
        }
    }

    public void SaveData(GameData data)
    {
        // doesnt perform saving that's done by Electricity Puzzle   
    }
}
