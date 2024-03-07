using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ConnectGame.Project.Scripts
{
    using UnityEngine;
    using System.IO;

    public class PuzzleDataFileHandler : MonoBehaviour
    {
        private string filePath;

        private void Start()
        {
            filePath = Application.persistentDataPath + "/PuzzleData.txt";
        }

        // Read difficulty setting from the file
        public int GetDifficulty()
        {
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                string[] parts = data.Split(',');

                if (int.TryParse(parts[0], out int difficulty))
                {
                    return difficulty;
                }
            }

            Debug.LogWarning("PuzzleData file not found or invalid format. Returning default difficulty.");
            return 1; // Default difficulty
        }

        // Read solved status from the file
        public bool GetSolvedStatus()
        {
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                string[] parts = data.Split(',');

                if (parts.Length > 1 && int.TryParse(parts[1], out int solvedInt))
                {
                    return solvedInt == 1;
                }
            }

            Debug.LogWarning("PuzzleData file not found or invalid format. Returning default solved status.");
            return false; // Default solved status
        }

        // Set and save new values to the file
        public void SetDifficultyAndSolvedStatus(int difficulty, bool solved)
        {
            string data = difficulty.ToString() + "," + (solved ? "1" : "0");
            File.WriteAllText(filePath, data);
        }
    }
}
