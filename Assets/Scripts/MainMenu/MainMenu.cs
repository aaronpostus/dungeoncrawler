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

        newGamePopUp.SetActive(true);
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();


        SceneManager.LoadSceneAsync("SampleScene");
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
}
