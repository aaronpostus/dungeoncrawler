using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using YaoLu;

public class NoteDeleter : MonoBehaviour
{
    //is true if the note is in the trigger of the button
    public bool canBePressed;

    private PlayerInput controls;

    public Vector2[] keys;

    //for debugging with a new size screen (if needed)
    private float initialY;    


    private void Awake()
    {
        controls = new PlayerInput();
        controls.Gameplay.Rhythm.Enable();

        keys = new Vector2[4];
        controls.Gameplay.Rhythm.performed += KeyPress;
        keys[0] = new Vector2 (-1f, 0f); //left
        keys[1] = new Vector2 (0f, 1f); //up
        keys[2] = new Vector2 (0f, -1f); //down
        keys[3] = new Vector2 (1f, 0f); //right

        initialY = transform.position.y;
    }

    public void OnDisable()
    {
        controls.Gameplay.Rhythm.Disable();
    }

    public void KeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (KeyCheck(context.ReadValue<Vector2>()))
            {
                //disables the note
                gameObject.SetActive(false);

                //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                RhythmManager.instance.HitArea(this.gameObject);
            }
        }
    }

    //called when note is in the trigger to be hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ArrowButton")
        {
            canBePressed = true;
        }
    }

    //called when note leaves button area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ArrowButton")
        {
            canBePressed = false;

            if (gameObject.activeSelf)
            {

                RhythmManager.instance.NoteMissed();

                //disables the note
                gameObject.SetActive(false);
            }
        }
    }

    //determines if the correct note was hit by the correct key
    private bool KeyCheck(Vector2 direction)
    {
        bool check = false;

        //Debug.Log(gameObject.transform.rotation.eulerAngles.z);

        if (gameObject.transform.rotation.eulerAngles.z == 180 && direction == keys[0])
        {
            check = true;
        }
        else if (gameObject.transform.rotation.eulerAngles.z == 90 && direction == keys[1])
        {
            check = true;
        }
        else if (gameObject.transform.rotation.eulerAngles.z == 270 && direction == keys[2])
        {
            check = true;
        }
        else if (gameObject.transform.rotation.eulerAngles.z == 0 && direction == keys[3])
        {
            check = true;
        }

        return check;
    }
}
