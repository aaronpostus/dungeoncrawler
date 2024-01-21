using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Loading : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private bool autoLoad;
    // Start is called before the first frame update
    void Start()
    {
        if (autoLoad)
        {
            SwitchScene();
        }
    }

    public void SwitchScene()
    {
        LevelManager.Instance.LoadScene(scene);
    }
}
