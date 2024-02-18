using System.Collections.Generic;
using UnityEngine;

public class NodePair : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public LayerMask objectLayerMask; // Optional: you can specify a layer mask to filter objects

    public List<GameObject> FindObjectsBetween()
    {
        List<GameObject> objectsBetween = new List<GameObject>();

        Vector3 direction = objectB.transform.position - objectA.transform.position;
        float distance = direction.magnitude;
        RaycastHit[] hits = Physics.RaycastAll(objectA.transform.position, direction, distance, objectLayerMask);

        foreach (RaycastHit hit in hits)
        {
            GameObject obj = hit.collider.gameObject;
            // Exclude objectA and objectB themselves
            if (obj != objectA && obj != objectB)
            {
                objectsBetween.Add(obj);
            }
        }

        return objectsBetween;
    }

    // Example usage
    public void ListObjectsBetween()
    {
        List<GameObject> objectsBetween = FindObjectsBetween();
        foreach (GameObject obj in objectsBetween)
        {
            Debug.Log(obj.name + " is between " + objectA.name + " and " + objectB.name);
        }
    }
}