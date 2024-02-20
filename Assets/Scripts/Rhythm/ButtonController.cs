using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using YaoLu;

public class ButtonController : MonoBehaviour
{
    //button attached to script
    private Button button;

    private PlayerInput controls;

    [SerializeField] private Vector2 arrowDirection;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        controls = new PlayerInput();
        controls.Gameplay.Rhythm.Enable();

        controls.Gameplay.Rhythm.performed += KeyPress;
        controls.Gameplay.Rhythm.canceled += KeyUp;
    }

    public void OnDisable()
    {
        controls.Gameplay.Rhythm.Disable();
    }

    public void KeyPress(InputAction.CallbackContext context)
    {
        if (arrowDirection == context.ReadValue<Vector2>())
        {
            FadeToColor(button.colors.pressedColor);
            button.onClick.Invoke();
        }
    }

    public void KeyUp(InputAction.CallbackContext context)
    {
        FadeToColor(button.colors.normalColor);
    }

    //fades the color of the button
    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }
}
