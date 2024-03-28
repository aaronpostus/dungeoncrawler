using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinimapCamBehaviour : MonoBehaviour
{
    public GameObject player;
    private PlayerInput controls;
    private float newSize;
    private float zoomSpeed;
    private float maxZoom;
    private float minZoom;

    private void Start()
    {
        maxZoom = 35f;
        minZoom = 1f;
        zoomSpeed = 20f;
        controls = new PlayerInput();
        controls.Minimap.Enable();


    }
    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 40, player.transform.position.z);
    }

    private void Update()
    {
            this.GetComponent<Camera>().orthographicSize += controls.Minimap.Zoom.ReadValue<float>() * Time.deltaTime * zoomSpeed;
            this.GetComponent<Camera>().orthographicSize = Mathf.Clamp(this.GetComponent<Camera>().orthographicSize, minZoom, maxZoom);
    }
}
