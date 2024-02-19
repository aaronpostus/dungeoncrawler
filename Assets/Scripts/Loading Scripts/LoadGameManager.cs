using Assets.Scripts.Loading_Scripts;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameManager : MonoBehaviour
{
    public static LoadGameManager instance { get; private set; }

    float currentProgress;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        instance = this;
    }

    public void LoadScene(string sceneName)
    {
       LoadSceneAsync(sceneName);
    }

    public async void LoadSceneAsync(string sceneName)
    {

        SceneManager.LoadScene("Loading Scene");

        await Task.Delay(500);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);

        } while (operation.progress < 0.9f);

        await Task.Delay(5000);

        operation.allowSceneActivation = true;
    }
}
