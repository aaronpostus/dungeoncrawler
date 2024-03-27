using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance { get; private set; }

    [Header("Menu Buttons")]
    
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitGameButton;

    [SerializeField] private GameObject newGamePopUp;
    [SerializeField] private GameObject quitGamePopUp;

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private string sceneToLoad;
    private void Start()
    {
        instance = this;

        if (!SaveGameManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();

        if (SaveGameManager.instance.HasGameData())
        {
            newGamePopUp.SetActive(true);
        }
        else
        {
            SaveGameManager.instance.NewGame();
        }
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();

        SaveGameManager.instance.ReturnToMainScene();
    }

    public void OnOptionsClicked()
    {
        DisableMenuButtons();

        mainMenu.SetActive(false);

        optionsMenu.SetActive(true);
    }

    public void OnQuitClicked()
    {
        DisableMenuButtons();

        quitGamePopUp.SetActive(true);
     }

    public void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
        optionsButton.interactable = false;
        quitGameButton.interactable = false;
    }

    public void EnableMenuButtons()
    {
        newGameButton.interactable = true;
        continueGameButton.interactable = true;
        quitGameButton.interactable = true;
        optionsButton.interactable = true;
    }

    //called in options menu OnReturnClicked method
    public void Return()
    {
        EnableMenuButtons();

        optionsMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}
