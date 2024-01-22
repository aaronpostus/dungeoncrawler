using Assets.Scripts.Loading_Scripts;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private string scene;
    [Tooltip("The image cycler is an optional component. If you do not add an image cycler the bar will load for a minimum of 5 seconds.")]
    [SerializeField] ImageCycler imageCycler;
    // Start is called before the first frame update
    void Start()
    {
        if (imageCycler) {
            LevelManager.Instance.LoadScene(scene, imageCycler);
            return;
        }
        LevelManager.Instance.LoadScene(scene);
    }
}
