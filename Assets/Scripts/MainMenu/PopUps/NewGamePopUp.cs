using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewGamePopUp : MonoBehaviour
{
    [SerializeField] private GameObject newGameButton;

    [SerializeField] private string sceneToLoad;

    public void StartNewGame()
    {
        SaveGameManager.instance.DeleteSave();

        SaveGameManager.instance.NewGame();

        //SceneManager.LoadSceneAsync("SampleScene");

        LoadGameManager.instance.LoadScene(sceneToLoad);
    }

    public void DoNotStartNewGame()
    {
        this.gameObject.SetActive(false);
        MainMenu.instance.EnableMenuButtons();
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }
}
