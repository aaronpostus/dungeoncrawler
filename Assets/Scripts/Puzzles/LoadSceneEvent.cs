

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Puzzles
{
    internal class LoadSceneEvent : InteractionEvent
    {
        [SerializeField] string sceneName;
        public override void Interact() {
            SceneManager.LoadScene(sceneName);
        }
    }
}
