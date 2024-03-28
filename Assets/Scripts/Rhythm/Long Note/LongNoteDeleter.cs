using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using YaoLu;

public class LongNoteDeleter : MonoBehaviour
{
    public bool canBePressed;
    public bool canBeReleased;
    public bool isBeingPressed;

    private bool leftTopHit;
    private bool rightTopHit;
    private bool upTopHit;
    private bool downTopHit;

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
            //Debug.Log("Pressed at right time!");
            if (gameObject.transform.GetChild(0).GetChild(0).rotation.eulerAngles.z == 90)
            {
                upTopHit = true;
            }
        }
    }

    public void DownKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            //Debug.Log("Pressed at right time!");
            if (gameObject.transform.GetChild(0).GetChild(0).rotation.eulerAngles.z == 270)
            {
                downTopHit = true;
            }
        }
    }
    public void LeftKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            //Debug.Log("Pressed at right time!");
            if (gameObject.transform.GetChild(0).GetChild(0).rotation.eulerAngles.z == 180)
            {
                leftTopHit = true;
            }
        }
    }
    public void RightKeyPress(InputAction.CallbackContext context)
    {
        if (canBePressed)
        {
            //Debug.Log("Pressed at right time!");
            if (gameObject.transform.GetChild(0).GetChild(0).rotation.eulerAngles.z == 0)
            {
                rightTopHit = true;
            }
        }
    }

    void Update()
    {
        if (controls.Rhythm.Left.IsPressed() || controls.Rhythm.Right.IsPressed() || controls.Rhythm.Down.IsPressed() || controls.Rhythm.Up.IsPressed())
        {
            isBeingPressed = true;
        }
        else
        {
            isBeingPressed = false;
        }

        //Debug.Log(isCorrectKeyReleased());
        if (canBeReleased && isCorrectKeyReleased())
        {
            //Debug.Log("Released");
            //disables the note
            gameObject.SetActive(false);

            //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

            RhythmManager.instance.HitAreaLongNote(this.gameObject.transform.GetChild(0).GetChild(1).gameObject);
        }

        if(isCorrectKeyReleased() && !canBeReleased)
        {
            leftTopHit = false;
            rightTopHit = false;
            upTopHit = false;
            downTopHit = false;

            if (gameObject.activeSelf)
            {

                RhythmManager.instance.NoteMissed();

                //disables the note
                gameObject.SetActive(false);
            }
        }
    }


    private bool isCorrectKeyReleased()
    {
        return (leftTopHit && !controls.Rhythm.Left.IsPressed()) || (rightTopHit && !controls.Rhythm.Right.IsPressed()) || (upTopHit && !controls.Rhythm.Up.IsPressed()) || (downTopHit && !controls.Rhythm.Down.IsPressed());
    }
}
