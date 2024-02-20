using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitGamePopUp : MonoBehaviour
{
    [SerializeField] private GameObject quitButton;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void DoNotQuitGame()
    {
        this.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(quitButton);
    }
}
