using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhythmManager : MonoBehaviour
{
    //music source
    public AudioSource music;

    //if true, start playing music
    private bool startPlaying;

    //note scroller to access hasStarted variable
    public NoteScroller noteScroller;

    //instance for other classes to call, only signle instance
    public static RhythmManager instance;

    //score variables
    private int currentScore;
    
    //scores per note hit
    private int scorePerNote = 100;
    public int scorePerPerfectNote = 150;

    //multiplier variables, noteTracker tracks amount of notes hit in a row
    public int currentMultiplier;
    public int mutliplierCombo;

    //threshold for how many notes to hit before increase in multiplier
    [SerializeField] public int amountOfThresholds = 3;
    public int[] noteThresholds;

    //text variables
    public TMP_Text scoreText;
    public TMP_Text multiplierText;

    void Start()
    {
        instance = this;

        noteThresholds = createThresholds();
        currentMultiplier = 1;
    }

    void Update()
    {
        //checks if it can start the rhythm section, can be used with other features like the battle sequence
        if (!startPlaying)
        {
            //checks if a key has been pressed before starting to play
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                noteScroller.hasStarted = true;

                music.Play();
            }
        }
    }

    //method called when any note is hit
    public void NoteHit()
    {
        //if the currentMultiplier is not at the max, it continues
        if(currentMultiplier - 1 < noteThresholds.Length)
        {
            //adds a note to the combo
            mutliplierCombo++;

            if (noteThresholds[currentMultiplier - 1] <= mutliplierCombo)
            {
                mutliplierCombo = 0;
                currentMultiplier++;
            }
        }

        //currentScore += scorePerNote * currentMultiplier;

        scoreText.text = "Score: " + currentScore;

        MultiplierText();

    }

    //method called when a hit is late
    public void LateHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    //method called when a hit is early
    public void EarlyHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    //method called when a hit is perfect
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    //method called when a note is missed
    public void NoteMissed()
    {
        //Debug.Log("Miss");

        //resets noteTacker and multiplier when a note is missed
        mutliplierCombo = 0;
        currentMultiplier = 1;
        MultiplierText();
    }

    //method called when multiplier text should be updated
    public void MultiplierText()
    {
        if (currentMultiplier == noteThresholds.Length)
        {
            multiplierText.text = "MAX MULTIPLIER!!";
        }
    }

    //method to create the thresholds (powers of 2), can be chaned with amountOfThresholds
    private int[] createThresholds()
    {
        int[] thresholds = new int[amountOfThresholds];

        thresholds[0] = 2;

        for (int i = 1; i < thresholds.Length; i++)
        {
            thresholds[i] = thresholds[i - 1] * 2;
        }

        return thresholds;
    }
}
