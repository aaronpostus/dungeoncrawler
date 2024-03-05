
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "ScriptableObjects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    // number from 1 to 5
    public int difficulty;
    public bool solved = false;
}