

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Puzzles
{
    internal class LoadPuzzleScene : InteractionEvent
    {
        [SerializeField] string sceneName;
        [SerializeField] Chest chest;
        [SerializeField] PuzzleData puzzleData;
        public override void Interact() {
            puzzleData._chestNumber = chest.chestNumber;
            puzzleData.difficulty = int.Parse(SceneManager.GetActiveScene().name);
            SaveGameManager.instance.TransitionAwayFromMainScene(sceneName);
        }
    }
}
