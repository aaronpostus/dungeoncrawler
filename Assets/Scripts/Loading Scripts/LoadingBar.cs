using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    private float currentProgress;

    private float fillSpeed = 1.0f;

    public void Update()
    {
        currentProgress = LoadGameManager.instance.Progress();
        //Debug.Log(currentProgress);
        loadingBar.value = Mathf.MoveTowards(loadingBar.value, currentProgress, fillSpeed * Time.deltaTime);
    }
}
