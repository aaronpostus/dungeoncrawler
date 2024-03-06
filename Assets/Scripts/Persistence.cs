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
        this.gameObject.SetActive(currentScene.name == "DunGenTest");
    }
}
