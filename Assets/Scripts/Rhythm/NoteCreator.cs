using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    //holds prefab of note to be instantiate
    public GameObject note;

    //game object of note that has already been instantiated
    private GameObject createdNote;

    //running amount of current notes created
    private int notesCreated;

    //ints for note values
    private int left = 1;
    private int up = 2;
    private int down = 3;
    private int right = 4;

    //buttons to determine note position
    [SerializeField] private GameObject leftButton, rightButton, upButton, downButton;

    //array for random note positions
    private List<int> notes;

    //int for amount of notes
    public int totalNotes = 0;

    //where the notes generate on screen
    private int generationPosition = -50;

    //hard coded for position between notes
    private int distanceBetweenNotes = 120;

    private List<int> noteSequences;

    void Start()
    {
        noteSequences = new List<int>();
    }

    // generates the notes for the attack
    public void GenerateNotes(int amountOfNotes)
    {
        //Debug.Log("NOTES CREATED!");
        if (notes == null)
        {
            totalNotes = amountOfNotes;
            notes = new List<int>();

            for (int i = 0; i < amountOfNotes; i++)
            {
                //generates ints between 1 and 4
                notes.Add(Random.Range(1, 5));
            }
        }
        else
        {
            totalNotes += amountOfNotes;

            for (int i = totalNotes - amountOfNotes;i < totalNotes; i++)
            {
                notes.Add(Random.Range(1, 5));
            }
        }

        addSequence(amountOfNotes);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            CreateNote();
        }
        else if (transform.childCount > 0 && transform.GetChild(notesCreated - 1).transform.position.y > generationPosition + distanceBetweenNotes)
        {
            //Debug.Log("Previous Child position: " + transform.GetChild(notesCreated - 1).transform.position.y);
            CreateNote();
        }
    }

    //creates a note when called 
    public void CreateNote()
    {
        if (notesCreated < totalNotes)
        {
            if (notes[notesCreated] == left)
            {
                createdNote = Instantiate(note, new Vector3(leftButton.transform.position.x, generationPosition, 0), Quaternion.Euler(0, 0, 180));
                createdNote.transform.SetParent(gameObject.transform, true);
            }
            else if (notes[notesCreated] == up)
            {
                createdNote = Instantiate(note, new Vector3(upButton.transform.position.x, generationPosition, 0), Quaternion.Euler(0, 0, 90));
                createdNote.transform.SetParent(gameObject.transform, true);
            }
            else if (notes[notesCreated] == down)
            {
                createdNote = Instantiate(note, new Vector3(downButton.transform.position.x, generationPosition, 0), Quaternion.Euler(0, 0, 270));
                createdNote.transform.SetParent(gameObject.transform, true);
            }
            else if (notes[notesCreated] == right)
            {
                createdNote = Instantiate(note, new Vector3(rightButton.transform.position.x, generationPosition, 0), Quaternion.Euler(0, 0, 0));
                createdNote.transform.SetParent(gameObject.transform, true);
            }

            notesCreated++;

            if (notesCreated == noteSequences[0])
            {
                createdNote.GetComponent<Outline>().enabled = true;
            }
            else if(notesCreated == totalNotes)
            {
                createdNote.GetComponent<Outline>().enabled = true;
            }
            else if (noteSequences.Count > 1 && (notesCreated - noteSequences[0]) % noteSequences[1] == 1 && notesCreated != noteSequences[0]+1)
            {
                createdNote.GetComponent<Outline>().enabled = true;
                noteSequences[0] += noteSequences[1];
                noteSequences.Remove(noteSequences[1]);
            }
        }
    }

    private void addSequence(int newSequence)
    {
        noteSequences.Add(newSequence);
    }
}
