using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private string scene;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.LoadScene(scene);
    }
}
