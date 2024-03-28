using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ISaveData
{
    [SerializeField] PuzzleData puzzleData;
    [SerializeField] Animator anim;
    [SerializeField] StringReference chestUnlocked;
    [SerializeField] GameObject triggerBox;

    public static int totalChests = 0;
    public int chestNumber;
    public bool isSolved = false;

    private GameData gameData;
    void Awake() {

        chestNumber = ++totalChests;
        Debug.Log("Chest number: " + totalChests);
    }
    public void LoadData(GameData data)
    {
        gameData = data;
        isSolved = gameData.chestsSolved[chestNumber].solved;
        Debug.Log("CHEST NO: " + chestNumber + " SOLVED: " + isSolved);
        if (isSolved)
        {
            Debug.Log(chestNumber + "is solved! Opening.");
            anim.SetBool("ChestOpen", true);
            Destroy(triggerBox);
        }
        // add itemdropped functionality
    }

    public void SaveData(GameData data)
    {
        // add itemdropped functionality

        data.chestsSolved[chestNumber] = (isSolved, false);
        totalChests = 0;
    }
}
