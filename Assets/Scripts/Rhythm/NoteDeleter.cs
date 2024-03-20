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
        controls.Rhythm.Enable();

        controls.Rhythm.Up.performed += UpKeyPress;
        controls.Rhythm.Down.performed += DownKeyPress;
        controls.Rhythm.Left.performed += LeftKeyPress;
        controls.Rhythm.Right.performed += RightKeyPress;

        initialY = transform.position.y;
    }

    public void OnDisable()
    {
        controls.Rhythm.Disable();
    }

    public void UpKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 90)
            {
                //disables the note
                gameObject.SetActive(false);

                //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                RhythmManager.instance.HitArea(this.gameObject);
            }
        }
    }

    public void DownKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 270)
            {
                //disables the note
                gameObject.SetActive(false);

                //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                RhythmManager.instance.HitArea(this.gameObject);
            }
        }
    }
    public void LeftKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 180)
            {
                //disables the note
                gameObject.SetActive(false);

                //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                RhythmManager.instance.HitArea(this.gameObject);
            }
        }
    }
    public void RightKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 0)
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
}
