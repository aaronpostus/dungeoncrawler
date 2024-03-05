using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    //bpm of song/beat used
    public float tempo;

    //true if the rhythm section has started
    public bool hasStarted;

    //multiply tempo of song by 33 due to size of screen, should be changed later when added to game
    public float screenMultiplier = 26.5f;


    // Start is called before the first frame update
    void Start()
    {
        //divide by 60 to get movement per second
        tempo = tempo * screenMultiplier / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            transform.position += new Vector3(0f, tempo * Time.deltaTime, 0f);
        }
    }
}
