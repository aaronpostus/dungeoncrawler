using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    Scene currentScene;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        // this object has zero references but if whoever is working on this needs to use this be sure to use SaveGameManager.instance.ReturnToMainScene() instead
        this.gameObject.SetActive(currentScene.name == "DunGenTest");
    }
}
