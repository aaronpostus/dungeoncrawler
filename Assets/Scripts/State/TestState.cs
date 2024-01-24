using UnityEngine;
using UnityEngine.InputSystem;

public class TestState : MonoBehaviour
{

    public TestState(InputAction quitAction)
    {
        quitAction.performed += TestAction_performed;
        quitAction.Enable();
    }

    private void TestAction_performed(InputAction.CallbackContext context)
    {
        EnterTestState();
    }

    private void EnterTestState()
    {
        // Implement the logic for entering the test state
        Debug.Log("Entered Test State");
        // Additional code for the test state
    }

    void Update()
    {
       
    }
}

