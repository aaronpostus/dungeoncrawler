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
    public float currentScore;
    
    //scores per note hit
    private float scorePerNote = 1f;
    private float scorePerPerfectNote = 1.5f;

    //multiplier variables, noteTracker tracks amount of notes hit in a row
    public int currentMultiplier;
    public int mutliplierCombo;

    //threshold for how many notes to hit before increase in multiplier
    [SerializeField] public int amountOfThresholds = 3;
    public int[] noteThresholds;

    //text variables
    public TMP_Text scoreText;
    public TMP_Text multiplierText;

    //left button to find y position to determine early/late/perfect/miss
    [SerializeField] private GameObject leftButton;

    //effects attached to prefabs   
    [SerializeField] private GameObject earlyEffect, lateEffect, perfectEffect, missEffect;

    //position where the text will generate
    [SerializeField] private GameObject textArea;

    //timer object
    [SerializeField] private Timer timer;

    [SerializeField] private NoteCreator noteCreator;

    private int notesDeleted;

    public bool continuePlaying;

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
            //checks if timer is done
            if (timer.timeRemaining == 0)
            {
                startPlaying = true;
                continuePlaying = true;
                noteScroller.hasStarted = true;

                //  music.Play();
            }
        }

        if (continuePlaying)
        {
            if(noteCreator.totalNotes > 0 && notesDeleted == noteCreator.totalNotes)
            {
                Debug.Log("Total Notes: " + noteCreator.totalNotes);
                continuePlaying = false;
            }
        }
    }

    public void HitArea(GameObject note)
    {
        if (note.transform.position.y < leftButton.transform.position.y - 5)
        {
            //Debug.Log("Early Hit");
            this.EarlyHit();
            Instantiate(earlyEffect, textArea.transform);

        }
        else if (note.transform.position.y > leftButton.transform.position.y + 5)
        {
            //Debug.Log("Late Hit");
            this.LateHit();
            Instantiate(lateEffect, textArea.transform);
        }
        else
        {
            //Debug.Log("Perfect Hit");
            this.PerfectHit();
            Instantiate(perfectEffect, textArea.transform);
        }
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

        Instantiate(missEffect, textArea.transform);

        notesDeleted++;

        //resets noteTacker and multiplier when a note is missed
        mutliplierCombo = 0;
        currentMultiplier = 1;
        MultiplierText();
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

            notesDeleted++;
        }

        //currentScore += scorePerNote * currentMultiplier;

        scoreText.text = "Score: " + currentScore;

        MultiplierText();

    }

    //method called when multiplier text should be updated
    public void MultiplierText()
    {
        if (currentMultiplier == noteThresholds.Length)
        {
            multiplierText.text = "MAX MULTIPLIER!!";
        }
        else
        {
            multiplierText.text = "Multiplier: x" + currentMultiplier;
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

    public void CreateNotes(int amountOfNotes, float tempo)
    {
        noteScroller.tempo = tempo;
        noteCreator.GenerateNotes(amountOfNotes);
    }
}
