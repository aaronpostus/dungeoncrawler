using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuPopUp : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject pauseMenuManager;

    public void ReturnToMainMenu()
    {
        LoadGameManager.instance.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void DoNotReturnToMainMenu()
    {
        this.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }
}
