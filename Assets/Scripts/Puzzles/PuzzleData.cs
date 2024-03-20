using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "ScriptableObjects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    // Fields to store difficulty and solved status
    [SerializeField] private int _difficulty = 1;
    public int difficulty
    {
        get { return _difficulty; }
        set
        {
            if (_difficulty != value)
            {
                _difficulty = value;
                SaveToFile();
            }
        }
    }

    [SerializeField] private bool _solved;
    public bool solved
    {
        get { return _solved; }
        set
        {
            if (_solved != value)
            {
                _solved = value;
                SaveToFile();
            }
        }
    }

    // File path to save/load data
    private string filePath;

    // Initialize file path and load data from file if it exists
    private void OnEnable()
    {
        filePath = Application.persistentDataPath + "/PuzzleData.txt";
        LoadFromFile();
    }

    // Save data to a text file
    private void SaveToFile()
    {
        // Create a string with the current difficulty and solved status
        string data = _difficulty.ToString() + "," + (_solved ? "1" : "0");

        // Write the data to the text file
        File.WriteAllText(filePath, data);
    }

    // Load data from a text file
    private void LoadFromFile()
    {
        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the data from the text file
            string data = File.ReadAllText(filePath);

            // Split the data into parts (difficulty and solved status)
            string[] parts = data.Split(',');

            // Parse the difficulty
            if (int.TryParse(parts[0], out int newDifficulty))
            {
                _difficulty = newDifficulty;
            }

            // Parse the solved status
            if (int.TryParse(parts[1], out int solvedInt))
            {
                _solved = solvedInt == 1;
            }
        }
        else
        {
            // If the file doesn't exist, save default values
            SaveToFile();
        }
    }
}