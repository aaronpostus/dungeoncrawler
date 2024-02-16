using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, ISaveData
{

    public bool visited;
    public bool currentlyVisited;

    public void Start()
    {
        currentlyVisited = false;
        visited = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Exo Gray")
        { 
            if (!visited)
            {
                visited = true;
                UpdateColor();
            }

            //need to figure out how to not save instantly when a game is loaded.
            SaveGameManager.instance.SaveGame();
        }
    }

    private void UpdateColor()
    {
        if (visited)
        {
            this.transform.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public void LoadData(GameData data)
    {
        if (data.checkpoints.ContainsKey(this.name))
        {
            visited = data.checkpoints[this.name];
        }

        UpdateColor();
    }

    public void SaveData(ref GameData data)
    {
        if (data.checkpoints.ContainsKey(this.name))
        {
            data.checkpoints[this.name] = visited;
        }
        else
        {
            data.checkpoints.Add(this.name, visited);
        }
    }
}