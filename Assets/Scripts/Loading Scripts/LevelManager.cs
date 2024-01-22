using Assets.Scripts.Loading_Scripts;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float fillSpeed;
    private float _target;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    public async void LoadScene(string sceneName, int duration)
    {
        //Start bar and target at 0
        _target = 0;
        _progressBar.fillAmount = 0;

        //Display loading screen canvas while scene loads
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        //Update the loading bar while the scene progress is below 0.9f (1.0f breaks this)
        do
        {
            await Task.Delay(100);
            _target = scene.progress;

        } while (scene.progress < 0.9f);

        //How long to display loading screen for after loaded
        await Task.Delay(duration);

        //Activate scene and turn off loading screen canvas
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }
    public void LoadScene(string sceneName, ImageCycler imageCycler)
    {
        Debug.Log(imageCycler.GetTotalDuration());
        LoadScene(sceneName, imageCycler.GetTotalDuration());
    }
    public void LoadScene(string sceneName)
    {
        LoadScene(sceneName, 5000);
    }
    private void Update()
    {
        
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, fillSpeed * Time.deltaTime);
    }
}
