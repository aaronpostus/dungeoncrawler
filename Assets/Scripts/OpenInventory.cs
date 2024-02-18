using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using YaoLu;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private bool paused;
    private InputAction inventory;
    private PlayerInput controls;

    void Awake()
    {
        controls = new PlayerInput();
    }

    private void OnEnable()
    {
        inventory = controls.Gameplay.OpenInventory;
        inventory.Enable();

        inventory.performed += Pause;
    }

    private void OnDisable()
    {
        inventory.Disable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        paused = !paused;

        if (paused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }
    void ActivateMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        AudioListener.pause = true;
        inventoryUI.SetActive(true);
    }

    void DeactivateMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        AudioListener.pause = true;
        inventoryUI.SetActive(false);
        paused = false;
    }
}
