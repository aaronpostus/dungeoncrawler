using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //player data
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    //item data
    public Vector3 ballPosition;

    //inital values of the game
    public GameData()
    {
        this.playerPosition = new Vector3();
        this.playerRotation = new Quaternion();

        this.ballPosition = new Vector3(2, 1, 5);
    }
}
