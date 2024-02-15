using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityPuzzle : MonoBehaviour
{
    public Transform puzzleHierarchy;
    public float rotationSpeed = 90f; // Adjust this to control the rotation speed (in degrees per second)

    private bool isRotating = false;

    public void RotateLeft()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCoroutine(Quaternion.Euler(0, -90, 0)));
        }
    }

    private IEnumerator RotateCoroutine(Quaternion targetRotation)
    {
        isRotating = true;
        Quaternion startRotation = puzzleHierarchy.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed / 90f; // Normalized time based on the rotation speed
            puzzleHierarchy.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        puzzleHierarchy.rotation = targetRotation; // Ensure we reach the exact target rotation
        isRotating = false;
    }
    public void RotateRight() { 
    
    }
}
