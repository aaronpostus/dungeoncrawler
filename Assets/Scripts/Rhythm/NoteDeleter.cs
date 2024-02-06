using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDeleter : MonoBehaviour
{
    //is true if the note is in the trigger of the button
    public bool canBePressed;

    //should be changed with new input system
    public KeyCode[] keys;

    //effects attached to prefabs   
    public GameObject earlyEffect, lateEffect, perfectEffect, missEffect;

    //for debugging with a new size screen (if needed)
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        keys = new KeyCode[4];
        keys[0] = KeyCode.LeftArrow;
        keys[1] = KeyCode.UpArrow;
        keys[2] = KeyCode.DownArrow;
        keys[3] = KeyCode.RightArrow;

        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (canBePressed)
        {
            if (KeyCheck())
            {
                //disables the note
                gameObject.SetActive(false);

                //Debug.Log("Initial position = " + initialY + ", current position = " + transform.position.y);

                if (transform.position.y < 147.5)
                {
                    //Debug.Log("Early Hit");
                    RhythmManager.instance.EarlyHit();
                    Instantiate(earlyEffect, transform.position, earlyEffect.transform.rotation);
                }
                else if (transform.position.y > 152.5)
                {
                    //Debug.Log("Late Hit");
                    RhythmManager.instance.LateHit();
                    Instantiate(lateEffect, transform.position, lateEffect.transform.rotation);
                }
                else
                {
                    //Debug.Log("Perfect Hit");
                    RhythmManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }

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
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);

                //disables the note
                gameObject.SetActive(false);
            }
        }
    }

    private bool KeyCheck()
    {
        bool check = false;

        //Debug.Log(gameObject.transform.rotation.eulerAngles.z);

        if (gameObject.transform.rotation.eulerAngles.z == 180 && Input.GetKeyDown(keys[0]))
        {
            check = true;
        }
        else if (gameObject.transform.rotation.eulerAngles.z == 90 && Input.GetKeyDown(keys[1]))
        {
            check = true;
        }
        else if (gameObject.transform.rotation.eulerAngles.z == 270 && Input.GetKeyDown(keys[2]))
        {
            check = true;
        }
        else if (gameObject.transform.rotation.eulerAngles.z == 0 && Input.GetKeyDown(keys[3]))
        {
            check = true;
        }

        return check;

    }
}
