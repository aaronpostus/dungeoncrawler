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

    //checkpoint data
    public bool[] visitedCheckpoints;
    public bool[] currentlyVisitedCheckpoints;

    //inital values of the game
    public GameData()
    {
        this.playerPosition = new Vector3();
        this.playerRotation = new Quaternion();

        this.ballPosition = new Vector3(5, 1, 2);

        this.visitedCheckpoints = new bool[CheckpointController.instance.checkpoints.Length];
        this.currentlyVisitedCheckpoints = new bool[CheckpointController.instance.checkpoints.Length];
    }
}
