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
    void Awake()
    {
        chestNumber = ++totalChests;
    }
    void Start() {
        if (isSolved)
        {
            anim.SetBool("ChestOpen", true);
            Destroy(triggerBox);
        }
    }
    public void LoadData(GameData data)
    {
        //isSolved = 
    }

    public void SaveData(GameData data)
    {
        
    }
}
