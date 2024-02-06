using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Exo Gray")
        {
            SaveGameManager.instance.SaveGame();

            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0));
        }
    }
}
