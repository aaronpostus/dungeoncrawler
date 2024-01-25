using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuQuit : MonoBehaviour
{
    [SerializeField] private GameObject _quitCanvas;
    [SerializeField] private GameObject _playButton;

    public void VerifyQuit()
    {
        _quitCanvas.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void DoNotQuitGame()
    {
        _quitCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_playButton);
    }
}
