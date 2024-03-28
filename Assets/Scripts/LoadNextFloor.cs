

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Puzzles
{
    internal class LoadNextFloor : InteractionEvent
    {
        public override void Interact() {
            LoadGameManager.instance.LoadSceneAsync(SaveGameManager.instance.AdvanceFloor() + "");
        }
    }
}
