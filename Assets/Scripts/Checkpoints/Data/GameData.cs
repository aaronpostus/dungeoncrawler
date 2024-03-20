using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //player data
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    //camera rotation
    public Quaternion cameraRotation;

    //checkpoint data
    public Dictionary<string, bool> checkpoints;

    public List<string> checkpointKeys;
    public List<bool> checkpointValues;

    //seed for dungen
    public int seed;

    //inital values of the game
    public GameData()
    {
        this.playerPosition = new Vector3();
        this.playerRotation = new Quaternion();

        this.cameraRotation = new Quaternion();

        checkpoints = new Dictionary<string, bool>();

        checkpointKeys = new List<string>();
        checkpointValues = new List<bool>();
    }
}
