using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "ScriptableObjects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    public float difficulty;
    public bool solved = false;
}
