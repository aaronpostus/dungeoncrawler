using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCreator : MonoBehaviour
{

    public GameObject note;

    private GameObject createdNote;

    private int notesCreated;

    //ints for note values
    private int left = 1;
    private int up = 2;
    private int down = 3;
    private int right = 4;

    //array for random note positions
    private int[] notes;

    //int for amount of notes
    public int amountOfNotes = 20;

    public int generationPosition = -235;

    // Start is called before the first frame update
    void Start()
    {
        notes = new int[amountOfNotes];

        for(int i = 0; i < amountOfNotes; i++)
        {
            notes[i] = Random.Range(1, 4);
        }

        Debug.Log(notes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            CreateNote();
        }
        else if (transform.childCount > 0 && transform.GetChild(notesCreated - 1).transform.position.y > generationPosition + 50)
        {
            CreateNote();
        }
    }

    //creates a note when called 
    public void CreateNote()
    {
        if (notesCreated < amountOfNotes)
        {
            if (notes[notesCreated] == left)
            {
                createdNote = Instantiate(note, new Vector3(-135, generationPosition, 0), Quaternion.Euler(0, 0, 180));
                createdNote.transform.SetParent(gameObject.transform, true);
            }
            else if (notes[notesCreated] == up)
            {
                createdNote = Instantiate(note, new Vector3(-35, generationPosition, 0), Quaternion.Euler(0, 0, 90));
                createdNote.transform.SetParent(gameObject.transform, true);
            }
            else if (notes[notesCreated] == down)
            {
                createdNote = Instantiate(note, new Vector3(65, generationPosition, 0), Quaternion.Euler(0, 0, 270));
                createdNote.transform.SetParent(gameObject.transform, true);
            }
            else if (notes[notesCreated] == right)
            {
                createdNote = Instantiate(note, new Vector3(165, generationPosition, 0), Quaternion.Euler(0, 0, 0));
                createdNote.transform.SetParent(gameObject.transform, true);
            }

            notesCreated++;
        }
    }
}
