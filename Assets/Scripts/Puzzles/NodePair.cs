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

        Vector3 minPosition = Vector3.Min(objectA.transform.position, objectB.transform.position);
        Vector3 maxPosition = Vector3.Max(objectA.transform.position, objectB.transform.position);

        Collider[] colliders = Physics.OverlapBox((minPosition + maxPosition) / 2f, (maxPosition - minPosition) / 2f, Quaternion.identity, objectLayerMask);

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
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