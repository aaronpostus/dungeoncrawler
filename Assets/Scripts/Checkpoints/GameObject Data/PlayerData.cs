using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, ISaveData
{
    private Vector3 temp;

    public void Start()
    {
        Debug.Log("THIS IS TEMP: " + temp.ToString());
        //this.transform.position = new Vector3(0f, 0f, 50f);
    }

    public void Update()
    { 

        if(this.transform.position.x == 0f && this.transform.position.z == 0f)
        {
            Debug.Log(this.transform.position.ToString());
            this.gameObject.transform.position = temp;
            Debug.Log(this.transform.position.ToString());
        }
    }

    public void LoadData(GameData data)
    {
        temp = data.playerPosition;
        //this.transform.position = new Vector3(0f, 50f, 0f);
        //this.transform.rotation = data.playerRotation; 
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        //data.playerRotation = this.transform.rotation;
    }
}
