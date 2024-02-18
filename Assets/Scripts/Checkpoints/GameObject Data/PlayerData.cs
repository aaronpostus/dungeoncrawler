using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, ISaveData
{

    public void LoadData(GameData data)
    {
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false;

        this.transform.position = data.playerPosition;
        this.transform.rotation = data.playerRotation; 

        cc.enabled = true;

        //this.transform.position = new Vector3(0f, 50f, 0f);
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerRotation = this.transform.rotation;
    }
}
