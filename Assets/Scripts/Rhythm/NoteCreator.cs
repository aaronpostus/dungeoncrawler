using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    //holds prefab of note to be instantiate
    public GameObject note;
    public GameObject heldNote;

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
    
    public List<int> noteSequences;

    private bool canDamage;

    private int sequenceTracker;
    private int currentSequence = 0;

    private GameObject createdIcon;
    public List<GameObject> icons;

    private GameObject createdLine;
    public List<GameObject> lines;

    [SerializeField] private GameObject line;

    private int iconXOffset = 150;
    private int lineXOffset = 50;
    private int lineYOffset = 50;

    private List<string> noteTypes;
    private string noteTypeNormal = "Normal";
    private string noteTypeHold = "Hold";

    void Start()
    {
        noteSequences = new List<int>();
        notes = new List<int>();
        noteTypes = new List<string>();
    }

    // generates the notes for the attack
    public void GenerateNotes(int amountOfNotes)
    {
        //Debug.Log("NOTES CREATED!");
        totalNotes += amountOfNotes;

        for (int i = totalNotes - amountOfNotes;i < totalNotes; i++)
        {
            notes.Add(Random.Range(1, 5));
            createRandomTypes(Random.Range(1, 3));
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
        else if (noteTypes[notesCreated - 1] == noteTypeNormal && transform.childCount > 0 && createdNote.transform.position.y > generationPosition + distanceBetweenNotes)
        {
            //Debug.Log("This is the " + notesCreated + " note. The previous child note was at " + transform.GetChild(notesCreated - 1).transform.position.y + ". The stored note was created at " + createdNote.transform.position.y);
            CreateNote();
        }
        else if (noteTypes[notesCreated - 1] == noteTypeHold && transform.childCount > 0 && createdNote.transform.GetChild(0).GetChild(1).position.y > generationPosition + distanceBetweenNotes)
        {
            //Debug.Log("This is the " + notesCreated + " note. The previous child note was at " + transform.GetChild(notesCreated - 1).transform.position.y + ". The stored note was created at " + createdNote.transform.position.y);
            CreateNote();
        }
    }

    //creates a note when called 
    public void CreateNote()
    {
        if (notesCreated < totalNotes)
        {
            Vector3 notePosition = new Vector3();
            Quaternion noteRotation = new Quaternion();

            notePosition.y = generationPosition;

            if (notes[notesCreated] == left)
            {
                notePosition.x = leftButton.transform.position.x;
                noteRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (notes[notesCreated] == up)
            {
                notePosition.x = upButton.transform.position.x;
                noteRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (notes[notesCreated] == down)
            {
                notePosition.x = downButton.transform.position.x;
                noteRotation = Quaternion.Euler(0, 0, 270);
            }
            else if (notes[notesCreated] == right)
            {
                notePosition.x = rightButton.transform.position.x;
                noteRotation = Quaternion.Euler(0, 0, 0);
            }

            createNoteType(notePosition, noteRotation);

            notesCreated++;
            sequenceTracker++;

            //Debug.Log("Current Sequence Val is " + currentSequence);
            //Debug.Log("Current Not Sequence is " + noteSequences[currentSequence]);

            if (sequenceTracker == noteSequences[currentSequence])
            {
                createLineOnType();
            }
        }
    }

    private void createNoteType(Vector3 position, Quaternion rotation)
    {
        if (noteTypes[notesCreated] == noteTypeNormal)
        {
            createdNote = Instantiate(note, position, rotation);
            createdNote.transform.SetParent(gameObject.transform, true);
        }
        else
        {
            createdNote = Instantiate(heldNote, position, Quaternion.Euler(0, 0, 0));
            createdNote.transform.GetChild(0).GetChild(0).rotation = rotation;
            createdNote.transform.GetChild(0).GetChild(1).rotation = rotation;
            createdNote.transform.SetParent(gameObject.transform, true);
        }
    }

    private void createRandomTypes(int randomTypeVal)
    {
        if(randomTypeVal == 1)
        {
            noteTypes.Add(noteTypeNormal);
        }
        else
        {
            noteTypes.Add(noteTypeHold);
        }

       // noteTypes.Add(noteTypeNormal);
    }

    private void createLineOnType()
    {
        if (noteTypes[notesCreated - 1] == noteTypeNormal)
        {
            createdIcon = Instantiate(QueueManager.instance.currentIcon(currentSequence), new Vector3(leftButton.transform.position.x - iconXOffset, generationPosition, 0), Quaternion.Euler(0, 0, 0));
            createdLine = Instantiate(line, new Vector3(downButton.transform.position.x - lineXOffset, generationPosition - lineYOffset, 0), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            GameObject bottomOfNote = createdNote.transform.GetChild(0).GetChild(1).gameObject;
            createdIcon = Instantiate(QueueManager.instance.currentIcon(currentSequence), new Vector3(leftButton.transform.position.x - iconXOffset, bottomOfNote.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            createdLine = Instantiate(line, new Vector3(downButton.transform.position.x - lineXOffset, bottomOfNote.transform.position.y - lineYOffset, 0), Quaternion.Euler(0, 0, 0));
        }
        
        createdIcon.transform.SetParent(gameObject.transform, true);

        createdLine.transform.SetParent(gameObject.transform, true);
        createdLine.GetComponent<Image>().color = createdIcon.GetComponent<Image>().color;

        icons.Add(createdIcon);
        lines.Add(createdLine);

        sequenceTracker = 0;
        //Debug.Log("Current Sequence Updated");
        currentSequence++;
    }

    private void addSequence(int newSequence)
    {
        noteSequences.Add(newSequence);
    }
}
