using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameEvent : InteractionEvent
{
    [SerializeField] GameObject saveSuccessfulPopUp;
    public override void Interact()
    {
        SaveGameManager.instance.SaveGame();

        saveSuccessfulPopUp.SetActive(true);
    }
}
