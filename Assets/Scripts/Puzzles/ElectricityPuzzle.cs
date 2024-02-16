using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityPuzzle : MonoBehaviour
{
    public Transform puzzleHierarchy;
    public float rotationSpeed = 90f; // Speed of rotation in degrees per second
    public bool rotateOnStart = false; // Whether to start rotating when the game starts

    private bool isRotating = false;
    private float startAngle; // Store the starting angle before rotating
    private float targetAngle;

    private void Update()
    {
        if (isRotating)
        {
            // Calculate the angle to rotate this frame
            float step = rotationSpeed * Time.deltaTime;
            float currentAngle = puzzleHierarchy.eulerAngles.y;
            float angleToRotate = Mathf.MoveTowardsAngle(currentAngle, targetAngle, step);

            // Rotate the object
            puzzleHierarchy.eulerAngles = new Vector3(0, angleToRotate, 0);

            // Check if rotation is complete
            if (Mathf.Abs(targetAngle - angleToRotate) < 0.01f)
            {
                isRotating = false;
            }
        }
    }

    public void RotateLeft()
    {
        if (!isRotating)
        {
            startAngle = puzzleHierarchy.eulerAngles.y;
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
            startAngle = puzzleHierarchy.eulerAngles.y;
            targetAngle = startAngle + 90f;

            // Ensure the angle is within [0, 360)
            if (targetAngle >= 360f)
                targetAngle -= 360f;

            // Start rotating
            isRotating = true;
        }
    }
}
