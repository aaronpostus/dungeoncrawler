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
    private int[] notes;

    //int for amount of notes
    private int totalNotes = 0;

    //where the notes generate on screen
    private int generationPosition = -50;

    //hard coded for position between notes
    private int distanceBetweenNotes = 120;

    void Start()
    {

    }

    // generates the notes for the attack
    public void GenerateNotes(int amountOfNotes)
    {
        Debug.Log("NOTES CREATED!");
        totalNotes = amountOfNotes;
        notes = new int[amountOfNotes];

        for(int i = 0; i < amountOfNotes; i++)
        {
            //generates ints between 1 and 4
            notes[i] = Random.Range(1, 5);
        }
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
        }
    }
}
