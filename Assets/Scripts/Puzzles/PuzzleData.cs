using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "ScriptableObjects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    // Fields to store difficulty and solved status
    public int _difficulty = 1;
    public int _chestNumber;
    public int difficulty
    {
        get { return _difficulty; }
        set
        {
            if (_difficulty != value)
            {
                _difficulty = value;
            }
        }
    }

    // Initialize file path and load data from file if it exists
    private void OnEnable()
    {

    }
}