using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _click;

    private FMOD.Studio.EventInstance click;

    private void Awake()
    {
        if (!_click.IsNull)
        {
            click = FMODUnity.RuntimeManager.CreateInstance(_click);
        }
    }
    public void ClickButton()
    {
        click.start();
    }
}
