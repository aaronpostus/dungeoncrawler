using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using YaoLu;

public class OpenMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject hud;
    [SerializeField] private bool paused;
    private InputAction menu;
    private PlayerInput controls;

    void Awake()
    {
        controls = new PlayerInput();
    }

    private void OnEnable()
    {
        menu = controls.Gameplay.OpenMenu;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        paused = !paused;

        if (paused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }
    void ActivateMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        hud.SetActive(false);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseUI.SetActive(true);
    }

    void DeactivateMenu()
    {
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
        hud.SetActive(true);
        Time.timeScale = 1f;
        AudioListener.pause = true;
        pauseUI.SetActive(false);
        paused = false;
    }
}
