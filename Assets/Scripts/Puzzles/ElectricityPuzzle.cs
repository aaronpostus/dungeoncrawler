using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElectricityPuzzle : MonoBehaviour
{
    public Transform puzzleParent; // Parent object containing all puzzle pieces
    public float rotationSpeed = 90f; // Speed of rotation in degrees per second
    public bool rotateOnStart = false; // Whether to start rotating when the game starts
    public List<NodePair> nodePairs;
    public List<Crystal> allCrystals;
    public List<ElectricBolt> electricBolts;

    [SerializeField] private float minDistance = 3f;
    [SerializeField] private Transform boundsMin;
    [SerializeField] private Transform boundsMax;

    private List<Crystal> crystals = new List<Crystal>();
    private bool isRotating = false;
    private float startAngle; // Store the starting angle before rotating
    private float targetAngle;
    private bool solved = false;

    public float rotationDuration = 5f;
    public TextMeshProUGUI uiText;
    public PuzzleData puzzleData;
    IEnumerator RotateObject()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = puzzleParent.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 360);

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);
            puzzleParent.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }


        // Wait for a few seconds
        yield return new WaitForSeconds(1f);

        // Transition to the next scene
        SaveGameManager.instance.ReturnToMainScene();
    }

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        List<ElectricBolt> boltsToDisable =  electricBolts.GetRange(puzzleData.difficulty, 5 - puzzleData.difficulty);
        foreach (ElectricBolt bolt in boltsToDisable) {
            bolt.enabled = false;
            Destroy(bolt.gameObject);
        }
        for (int i = puzzleData.difficulty * 2; i < 10; i++) {
            allCrystals[i].gameObject.SetActive(false);
            Debug.Log("f");
        }
        allCrystals.RemoveRange(puzzleData.difficulty * 2, 10 - (puzzleData.difficulty * 2));
        List<NodePair> pairsToDisable = nodePairs.GetRange(puzzleData.difficulty, 5 - puzzleData.difficulty);
        foreach (NodePair pair in pairsToDisable)
        {
            nodePairs.Remove(pair);
            pair.Delete();

        }

        MoveObjectsToRandomPositions();
    }
    private void MoveObjectsToRandomPositions()
    {
        List<GameObject> objectsToMove = new List<GameObject>();
        foreach (Crystal crystal in allCrystals) {
            objectsToMove.Add(crystal.gameObject);
        }
        foreach (NodePair pair in nodePairs) {
            objectsToMove.Add(pair.GetNode1());
            objectsToMove.Add(pair.GetNode2());
        }
        foreach (var obj in objectsToMove)
        {
            Vector3 randomPosition = GetRandomPosition();
            obj.transform.position = randomPosition;
        }
    }

    private Vector3 GetRandomPosition()
    {
        int maxAttempts = 10;
        int attempts = 0;
        Vector3 randomPosition = Vector3.zero;

        while (attempts < maxAttempts)
        {
            float x = Random.Range(boundsMin.position.x, boundsMax.position.x);
            float y = Random.Range(boundsMin.position.y, boundsMax.position.y);
            float z = Random.Range(boundsMin.position.z, boundsMax.position.z);

            randomPosition = new Vector3(x, y, z);

            Collider[] colliders = Physics.OverlapSphere(randomPosition, minDistance);
            bool positionIsValid = true;
            foreach (var collider in colliders)
            {
                if (collider != null && collider != boundsMin.GetComponent<Collider>() && collider != boundsMax.GetComponent<Collider>())
                {
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid)
                return randomPosition;

            attempts++;
        }

        Debug.LogWarning("Max attempts reached to find a valid position. Returning a position regardless.");
        return randomPosition;
    }
    private void OnDrawGizmosSelected()
    {
        if (boundsMin != null && boundsMax != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube((boundsMin.position + boundsMax.position) / 2, boundsMax.position - boundsMin.position);
        }
    }

    private void Update()
    {
        if (solved) {
            return;
        }
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

        foreach (Crystal crystal in allCrystals) {
            if (!crystal.IsPowered()) {
                return;
            }
        }

        // Delete all DraggableNode scripts in the scene
        DraggableNode[] draggableNodes = FindObjectsOfType<DraggableNode>();
        foreach (DraggableNode node in draggableNodes)
        {
            Destroy(node);
        }
        solved = true;
        // Start rotating the object
        StartCoroutine(RotateObject());
        uiText.enabled = true;
        //puzzleData.solved = true;
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
