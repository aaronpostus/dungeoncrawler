using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using YaoLu;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button closeMenuButton;

    [SerializeField] private GameObject quitGamePopUp;
    [SerializeField] private GameObject mainMenuPopUp;

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;

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

    public void OnOptionsClicked()
    {
        DisableMenuButtons();

        pauseMenu.SetActive(false);

        optionsMenu.SetActive(true);
    }

    public void OnReturnClicked()
    {
        EnableMenuButtons();

        optionsMenu.SetActive(false);

        pauseMenu.SetActive(true);
    }

    public void OnMainMenuClicked()
    {
        DisableMenuButtons();

        mainMenuPopUp.SetActive(true);
    }

    public void OnQuitClicked()
    {
        DisableMenuButtons();

        quitGamePopUp.SetActive(true);
    }

    public void OnCloseMenuClicked()
    {
        DisableMenuButtons();

        DeactivateMenu();
    }

    public void DisableMenuButtons()
    {
        optionsButton.interactable = false;
        mainMenuButton.interactable = false;
        quitGameButton.interactable = false;
        closeMenuButton.interactable = false;
    }

    public void EnableMenuButtons()
    {
        optionsButton.interactable = true;
        mainMenuButton.interactable = true;
        quitGameButton.interactable = true;
        closeMenuButton.interactable = true;
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
        EnableMenuButtons();
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
        Cursor.lockState = CursorLockMode.Locked;
        hud.SetActive(true);
        Time.timeScale = 1f;
        AudioListener.pause = true;
        pauseUI.SetActive(false);
        paused = false;
    }
}
