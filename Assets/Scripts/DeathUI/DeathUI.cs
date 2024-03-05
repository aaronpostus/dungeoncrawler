using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    public void OnRespawnClicked()
    {
        LoadGameManager.instance.LoadScene("DunGenTest");
    }

    public void OnMainMenuClicked()
    {
        LoadGameManager.instance.LoadScene("MainMenu");
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
