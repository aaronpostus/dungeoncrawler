﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using YaoLu;

public class InteractBoxTrigger : MonoBehaviour
{
    public Collider interactCollider; // Reference to the collider for interaction
    public StringReference interactText;
    private FSMCtrl fsmCtrl;
    private bool playerWithinCollider = false;
    [SerializeField] string interactString = "Interact";
    [SerializeField] InteractionEvent interactEvent;
    private void Start()
    {
        // Find the GameObject with FSMCtrl script attached
        GameObject fsmCtrlObject = GameObject.FindWithTag("Player");

        if (fsmCtrlObject != null)
        {
            // Get the FSMCtrl component attached to the GameObject
            fsmCtrl = fsmCtrlObject.GetComponent<FSMCtrl>();

            if (fsmCtrl != null)
            {
                // Subscribe to the Interact event
                fsmCtrl.AddInteractListener(OnInteractHandler);
            }
            else
            {
                Debug.LogError("FSMCtrl component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with FSMCtrl script not found.");
        }
    }

    private void OnInteractHandler(InputAction.CallbackContext context)
    {
        if (playerWithinCollider)
        {
            interactEvent.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerWithinCollider = true;
            interactText.Value = "[E] " + interactString;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerWithinCollider = false;
            interactText.Value = string.Empty;
        }
    }
}