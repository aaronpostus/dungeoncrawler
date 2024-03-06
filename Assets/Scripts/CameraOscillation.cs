using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOscillation : MonoBehaviour
{
    public GameObject target;
 
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(Vector3.right * Time.deltaTime);
    }
}
