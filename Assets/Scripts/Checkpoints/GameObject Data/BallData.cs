using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData : MonoBehaviour, ISaveData
{
    public void LoadData(GameData data)
    {
        this.transform.position = data.ballPosition;
    }

    public void SaveData(GameData data)
    {
        data.ballPosition = this.transform.position;
    }
}
