using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
namespace YaoLu
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput inputScheme;
        private QuitHandler quitHandler;
        private bool isPaused = false;
        private bool isMuted = false;

        private void Awake()
        {
            inputScheme = new PlayerInput();

            // Setup quit handler
            quitHandler = new QuitHandler(inputScheme.Gameplay.Quit);

            // Subscribe to pause action
            inputScheme.Gameplay.Pause.performed += HandlePausePerformed;
            inputScheme.Gameplay.Mute.performed += HandleMutePerformed;
        }

        private void OnEnable()
        {
            inputScheme.Enable();
        }

        private void OnDisable()
        {
            inputScheme.Disable();
        }

        private void HandlePausePerformed(InputAction.CallbackContext context)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                // Pause the game
                Time.timeScale = 0;
                // Additional pause logic here (e.g., show pause menu)
            }
            else
            {
                // Resume the game
                Time.timeScale = 1;
                // Additional resume logic here (e.g., hide pause menu)
            }
        }
        private void HandleMutePerformed(InputAction.CallbackContext context)
        {
            isMuted = !isMuted;
            if (isMuted)
            {
                // Mute the audio
            }
            else
            {
                // Unmute the audio
            }
        }
    }
}
