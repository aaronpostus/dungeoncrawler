

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Puzzles
{
    internal class LoadSceneEvent : InteractionEvent
    {
        [SerializeField] string sceneName;
        [SerializeField] bool returnToMainScene;
        public override void Interact() {
            if (returnToMainScene) {
                SaveGameManager.instance.ReturnToMainScene();
                return;
            }
            SaveGameManager.instance.TransitionAwayFromMainScene(sceneName);
        }
    }
}
