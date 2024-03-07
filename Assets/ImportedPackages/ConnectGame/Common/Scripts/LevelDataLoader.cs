using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Connect.Common;

public class LevelDataLoader : MonoBehaviour
{
    public static List<LevelData> allLevelData = new List<LevelData>();

    private void Awake()
    {
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        allLevelData.Clear(); // Clear the list to avoid duplicates

        string directoryPath = "Assets/ConnectGame/Common/Prefabs/Levels"; // Path to the directory containing LevelData scriptable objects
        DirectoryInfo directory = new DirectoryInfo(directoryPath);

        if (!directory.Exists)
        {
            Debug.LogError("Directory not found: " + directoryPath);
            return;
        }

        // Get all files in the directory
        FileInfo[] files = directory.GetFiles("*.asset");

        foreach (FileInfo file in files)
        {
            string assetPath = "Assets/ConnectGame/Common/Prefabs/Levels/" + file.Name;
            LevelData levelData = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelData>(assetPath);

            if (levelData != null)
            {
                allLevelData.Add(levelData);
            }
            else
            {
                Debug.LogWarning("Failed to load LevelData from file: " + file.Name);
            }
        }
    }
}