using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using YaoLu;

public class LongNoteDeleter : MonoBehaviour
{
    public bool canBePressed;
    public bool canBeReleased;
    private bool topHit;

    private PlayerInput controls;

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

    public void UpKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 90)
            {
                if(canBeReleased)
                {
                    //disables the note
                    gameObject.SetActive(false);

                    //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                    RhythmManager.instance.HitArea(this.gameObject);
                }
            }
        }
    }

    public void DownKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 270)
            {
                if (canBeReleased)
                {
                    //disables the note
                    gameObject.SetActive(false);

                    //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                    RhythmManager.instance.HitArea(this.gameObject);
                }
            }
        }
    }
    public void LeftKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            Debug.Log("Pressed at right time!");
            if (gameObject.transform.rotation.eulerAngles.z == 180)
            {
                topHit = true;
            }
        }
    }
    public void RightKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            if (gameObject.transform.rotation.eulerAngles.z == 0)
            {
                if (canBeReleased)
                {
                    //disables the note
                    gameObject.SetActive(false);

                    //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                    RhythmManager.instance.HitAreaLongNote(this.gameObject);
                }
            }
        }
    }

    void Update()
    {
        if (canBeReleased && topHit && !controls.Rhythm.Left.IsPressed())
        {
            Debug.Log("Released");
            //disables the note
            gameObject.SetActive(false);

            //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

            RhythmManager.instance.HitAreaLongNote(this.gameObject);
        }

        if(topHit && !controls.Rhythm.Left.IsPressed() && !canBeReleased)
        {
            topHit = false;


            if (gameObject.activeSelf)
            {

                RhythmManager.instance.NoteMissed();

                //disables the note
                gameObject.SetActive(false);
            }
        }
    }
}
