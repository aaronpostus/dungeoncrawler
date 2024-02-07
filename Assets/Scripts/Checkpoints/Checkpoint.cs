using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
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
            CheckpointController.instance.currentVisitUpdate(this.gameObject);

            SaveGameManager.instance.SaveGame();

            if (!visited)
            {
                visited = true;
            }

            CheckpointController.instance.checkpointUpdate();
        }
    }
}