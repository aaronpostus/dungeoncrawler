using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //set this to initial time value
    public float timeRemaining;

    public bool timerDisplayed;

    [SerializeField] private TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerDisplayed = true;
        timerText.gameObject.SetActive(timerDisplayed);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplayed)
        {
            //it will show 0 if 0 is used instead of .01 
            if(timeRemaining > .01)
            {
                timeRemaining -= Time.deltaTime;
                updateTimer(timeRemaining);
            }
            else
            {
                //Debug.Log("Time is UP!");
                timeRemaining = 0;
                timerDisplayed = false;
                timerText.gameObject.SetActive(timerDisplayed);
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text =  seconds.ToString();
    }
}
