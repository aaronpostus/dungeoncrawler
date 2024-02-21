using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableCable : MonoBehaviour
{
    void Update()
    {
        // Check if the mouse button is clicked
        if (Input.GetMouseButtonDown(0)) // Change to 1 for right click, 2 for middle click
        {
            // Check if we clicked on this game object
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Rotate the object by 90 degrees around its up axis (y-axis)
                    transform.Rotate(Vector3.back, 90f, Space.Self);
                }
            }
        }
    }
}
