using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using YaoLu;

public class ButtonController : MonoBehaviour
{
    //button attached to script
    private Button leftButton, upButton, downButton, rightButton;

    private PlayerInput controls;

    [SerializeField] private Vector2 arrowDirection;

    // Start is called before the first frame update
    void Start()
    {
        leftButton = transform.GetChild(0).gameObject.GetComponent<Button>(); //left
        upButton = transform.GetChild(1).gameObject.GetComponent<Button>(); //up
        downButton = transform.GetChild(2).gameObject.GetComponent<Button>(); //down
        rightButton = transform.GetChild(3).gameObject.GetComponent<Button>(); //right

        controls = new PlayerInput();
        controls.Rhythm.Enable();

        controls.Rhythm.Up.performed += KeyUpPress;
        controls.Rhythm.Up.canceled += KeyUpRelease;

        controls.Rhythm.Down.performed += KeyDownPress;
        controls.Rhythm.Down.canceled += KeyDownRelease;

        controls.Rhythm.Left.performed += KeyLeftPress;
        controls.Rhythm.Left.canceled += KeyLeftRelease;

        controls.Rhythm.Right.performed += KeyRightPress;
        controls.Rhythm.Right.canceled += KeyRightRelease;
    }

    public void OnDisable()
    {
        controls.Rhythm.Disable();
    }

    public void KeyUpPress(InputAction.CallbackContext context)
    {
        FadeToColor(upButton, upButton.colors.pressedColor);
        upButton.onClick.Invoke();
    }

    public void KeyUpRelease(InputAction.CallbackContext context)
    {
        FadeToColor(upButton, upButton.colors.normalColor);
    }


    public void KeyDownPress(InputAction.CallbackContext context)
    {
        FadeToColor(downButton, downButton.colors.pressedColor);
        downButton.onClick.Invoke();
    }

    public void KeyDownRelease(InputAction.CallbackContext context)
    {
        FadeToColor(downButton, downButton.colors.normalColor);
    }

    public void KeyLeftPress(InputAction.CallbackContext context)
    {
        FadeToColor(leftButton, leftButton.colors.pressedColor);
        leftButton.onClick.Invoke();
    }

    public void KeyLeftRelease(InputAction.CallbackContext context)
    {
        FadeToColor(leftButton, leftButton.colors.normalColor);
    }

    public void KeyRightPress(InputAction.CallbackContext context)
    {
        FadeToColor(rightButton, rightButton.colors.pressedColor);
        rightButton.onClick.Invoke();
    }

    public void KeyRightRelease(InputAction.CallbackContext context)
    {
        FadeToColor(rightButton, rightButton.colors.normalColor);
    }

    //fades the color of the button
    void FadeToColor(Button currentButton, Color color)
    {
        Graphic graphic = currentButton.GetComponent<Graphic>();
        graphic.CrossFadeColor(color, currentButton.colors.fadeDuration, true, true);
    }
}
