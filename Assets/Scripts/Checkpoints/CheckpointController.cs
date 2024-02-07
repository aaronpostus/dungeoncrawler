using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour, ISaveData
{
    public GameObject[] checkpoints;

    private Checkpoint[] checkpointScripts;

    public static CheckpointController instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        checkpointScripts = new Checkpoint[checkpoints.Length];

        for (int i = 0; i < checkpoints.Length; i++)
        {

            //Debug.Log(checkpoints[i].name);
            checkpointScripts[i] = checkpoints[i].GetComponent<Checkpoint>();
            //Debug.Log(checkpointScripts[i]);
        }

        checkpointUpdate();
    }

    public void checkpointUpdate()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpointScripts[i].currentlyVisited)
            {
                checkpointScripts[i].transform.GetComponent<Renderer>().material.color = Color.black;
            }
            else if (checkpointScripts[i].visited)
            {
                checkpointScripts[i].currentlyVisited = false;
                checkpoints[i].transform.GetComponent<Renderer>().material.color = Color.gray;
            }
        }
    }

    public void currentVisitUpdate(GameObject currentCheckpoint)
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].name != currentCheckpoint.name)
            {
                checkpointScripts[i].currentlyVisited = false;
            }
            else
            {
                checkpointScripts[i].currentlyVisited = true;
            }
        }
    }

    public void LoadData(GameData data)
    {
        for(int i = 0; i < checkpointScripts.Length; i++)
        {
            checkpointScripts[i].visited = data.visitedCheckpoints[i];
            checkpointScripts[i].currentlyVisited = data.currentlyVisitedCheckpoints[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < checkpointScripts.Length; i++)
        {
            data.visitedCheckpoints[i] = checkpointScripts[i].visited;
            data.currentlyVisitedCheckpoints[i] = checkpointScripts[i].currentlyVisited;
        }
    }
}
