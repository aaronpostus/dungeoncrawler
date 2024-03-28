using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //levelspecificdata
    public int currentLevel;

    //player data
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    //camera rotation
    public Quaternion cameraRotation;

    //checkpoint data
    public Dictionary<string, bool> checkpoints;

    public List<string> checkpointKeys;
    public List<bool> checkpointValues;
    
    // solved specifies if the chest should be open, itemdropped specifies if there should be an item dropped. after an item has been picked up itemDropped is false.

    public Dictionary<int,(bool solved, bool itemDropped)> chestsSolved;

    public List<int> chestKeys;
    public List<bool> chestSolved;
    public List<bool> chestItemDropped;

    //seed for dungen
    public int seed;

    //enemy data
    public string currentEnemyType;
    //inital values of the game
    public GameData()
    {
        currentLevel = 1;
        playerPosition = new Vector3();
        playerRotation = new Quaternion();

        chestsSolved = new Dictionary<int, (bool,bool)>();

        chestKeys = new List<int>();
        chestSolved = new List<bool>();
        chestItemDropped = new List<bool>();

        cameraRotation = new Quaternion();

        checkpoints = new Dictionary<string, bool>();

        checkpointKeys = new List<string>();
        checkpointValues = new List<bool>();
    }
}
