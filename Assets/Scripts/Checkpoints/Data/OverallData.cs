using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallData : MonoBehaviour, ISaveData
{
    public GameObject player;
    public GameObject ball;

    public void LoadData(GameData data)
    {
        this.player.transform.position = data.playerPosition;
        this.player.transform.rotation = data.playerRotation;

        this.ball.transform.position = data.ballPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.player.transform.position;
        data.playerRotation = this.player.transform.rotation;

        data.ballPosition = this.ball.transform.position;
    }
}
