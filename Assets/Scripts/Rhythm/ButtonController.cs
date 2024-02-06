using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    //button attached to script
    private Button button;

    //should be changed with new input system
    public KeyCode arrow;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(arrow))
        {
            FadeToColor(button.colors.pressedColor);
            button.onClick.Invoke();
        }
        else if(Input.GetKeyUp(arrow)) 
        {
            FadeToColor(button.colors.normalColor);
        }      
    }

    //fades the color of the button
    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }
}
