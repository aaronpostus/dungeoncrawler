using System.Collections.Generic;
using UnityEngine;

public class NodePair : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public Crystal.CrystalType type;
    public LayerMask objectLayerMask; // Optional: you can specify a layer mask to filter objects
    private List<Crystal> gameObjs;
    
    public void Awake()
    {
        gameObjs = new List<Crystal>();
    }
    public void Delete() {
        objectA.SetActive(false);
        objectB.SetActive(false);
        Destroy(this.gameObject);
    }
    public GameObject GetNode1() { 
        return objectA;
    }
    public GameObject GetNode2()
    {
        return objectB;
    }
    public List<Crystal> FindObjectsBetween()
    {
        List<Crystal> objectsBetween = new List<Crystal>();

        Vector3 direction = objectB.transform.position - objectA.transform.position;
        float distance = direction.magnitude;
        RaycastHit[] hits = Physics.RaycastAll(objectA.transform.position, direction, distance, objectLayerMask);

        foreach (RaycastHit hit in hits)
        {
            Crystal obj = hit.collider.gameObject.GetComponent<Crystal>();
            // Exclude objectA and objectB themselves
            if (obj != objectA && obj != objectB && obj.GetType() == type)
            {
                objectsBetween.Add(obj);
            }
        }
        return objectsBetween;
    }
}