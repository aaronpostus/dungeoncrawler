using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour, ISaveData
{
    public void LoadData(GameData data)
    {
        Debug.Log("Data Loaded");
        this.transform.rotation = data.cameraRotation;
    }

    public void SaveData(GameData data)
    {
        data.cameraRotation = this.transform.rotation;
    }
}
