using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PuzzleData puzzleData;
    void Start()
    {
        if (puzzleData.solved) {
            anim.SetBool("Open", true);
        }
    }
}
