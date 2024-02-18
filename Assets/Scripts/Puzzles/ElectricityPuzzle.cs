using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityPuzzle : MonoBehaviour
{
    public Transform puzzleParent; // Parent object containing all puzzle pieces
    public float rotationSpeed = 90f; // Speed of rotation in degrees per second
    public bool rotateOnStart = false; // Whether to start rotating when the game starts
    public List<NodePair> nodePairs;

    private List<Crystal> crystals = new List<Crystal>();
    private bool isRotating = false;
    private float startAngle; // Store the starting angle before rotating
    private float targetAngle;

    private void Update()
    {
        if (isRotating)
        {
            // Calculate the angle to rotate this frame
            float step = rotationSpeed * Time.deltaTime;
            float currentAngle = puzzleParent.eulerAngles.y;
            float angleToRotate = Mathf.MoveTowardsAngle(currentAngle, targetAngle, step);

            // Rotate the parent object
            puzzleParent.eulerAngles = new Vector3(0, angleToRotate, 0);

            // Check if rotation is complete
            if (Mathf.Abs(targetAngle - angleToRotate) < 0.01f)
            {
                isRotating = false;
            }
        }
        // List to hold the crystals from the previous frame
        List<Crystal> oldCrystals = new List<Crystal>(crystals);
        // Create a new list to store the updated crystals
        List<Crystal> updatedCrystals = new List<Crystal>();

        foreach (NodePair pair in nodePairs)
        {
            foreach (Crystal crystal in pair.FindObjectsBetween())
            {
                // Check if the crystal is not already in the updated list
                if (!updatedCrystals.Contains(crystal))
                {
                    updatedCrystals.Add(crystal);
                    // Check if the crystal was not in the old list
                    if (!oldCrystals.Contains(crystal))
                    {
                        crystal.Power();
                    }
                }
            }
        }

        // Now check for crystals that were in the old list but not in the updated list
        foreach (Crystal oldCrystal in oldCrystals)
        {
            if (!updatedCrystals.Contains(oldCrystal))
            {
                oldCrystal.StopPowering();
            }
        }

        // Update the oldCrystals list with the updatedCrystals list for the next frame
        crystals = new List<Crystal>(updatedCrystals);

    }

    public void RotateLeft()
    {
        if (!isRotating)
        {
            startAngle = puzzleParent.eulerAngles.y;
            targetAngle = startAngle - 90f;

            // Ensure the angle is within [0, 360)
            if (targetAngle < 0f)
                targetAngle += 360f;

            // Start rotating
            isRotating = true;
        }
    }

    public void RotateRight()
    {
        if (!isRotating)
        {
            startAngle = puzzleParent.eulerAngles.y;
            targetAngle = startAngle + 90f;

            // Ensure the angle is within [0, 360)
            if (targetAngle >= 360f)
                targetAngle -= 360f;

            // Start rotating
            isRotating = true;
        }
    }
}
