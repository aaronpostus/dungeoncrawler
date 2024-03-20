using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] PuzzleData puzzleData;
    [SerializeField] Animator anim;
    [SerializeField] StringReference chestUnlocked;
    [SerializeField] GameObject triggerBox;
    // Start is called before the first frame update
    void Awake()
    {
        if (puzzleData.solved) {
            anim.SetBool("ChestOpen", true);
            chestUnlocked.Value = "Opened chest and unlocked item.";
            Destroy(triggerBox);
            StartCoroutine(RemoveText());
        }
    }
    IEnumerator RemoveText() {
        yield return new WaitForSeconds(2f);
        if (chestUnlocked.Value == "chestUnlocked.Value") {
            chestUnlocked.Value = "";
        }
        yield return null;
    }
}
