using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private float lifetime = .5f;

    // Update is called once per frame
    void Update()
    {
        Destroy (gameObject, lifetime);
    }
}
