using UnityEngine;

public class InitializeScene : MonoBehaviour
{
    [SerializeField] private string scene;
    private void Awake()
    {
        ChangeScene(scene);
    }
    public void ChangeScene(string sceneName)
    {
        LevelManager.Instance.LoadScene(sceneName);
    }
}
