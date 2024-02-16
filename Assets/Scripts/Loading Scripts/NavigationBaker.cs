using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour {

    public NavMeshSurface surfaces;

    // Use this for initialization
    void Start () 
    {
            surfaces.BuildNavMesh (); 
    }

}
