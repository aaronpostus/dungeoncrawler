using Assets.Scripts.Loading_Scripts;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Loading : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private bool autoLoad;
    [SerializeField] ImageCycler imageCycler;
    [Tooltip("The image cycler is an optional component. If you do not add an image cycler the bar will load for a minimum of 5 seconds.")]
    // Start is called before the first frame update
    void Start()
    {
        if (imageCycler)
        {
            LevelManager.Instance.LoadScene(scene, imageCycler);
            return;
        }
        else
        {
            if (autoLoad)
            {
                SwitchScene();
            }
        }
    }

    public void SwitchScene()
    {
        Debug.Log("Switching to " + scene);
        LevelManager.Instance.LoadScene(scene);
    }
}
