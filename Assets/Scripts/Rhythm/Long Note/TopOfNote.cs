using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfNote : MonoBehaviour
{
    public LongNoteDeleter longNoteDeleter;
    //called when note is in the trigger to be hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ArrowButton")
        {
            longNoteDeleter.canBePressed = true;
        }
    }

    //called when note leaves button area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ArrowButton")
        {
            longNoteDeleter.canBePressed = false;
        }
    }
}
