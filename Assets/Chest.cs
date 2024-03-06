using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] PuzzleData puzzleData;
    [SerializeField] Animator anim;
    [SerializeField] GameObject triggerBox;
    // Start is called before the first frame update
    void Awake()
    {
        if (puzzleData.solved) {
            anim.SetBool("ChestOpen", true);
            Destroy(triggerBox);
        }
    }
}
