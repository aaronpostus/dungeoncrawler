using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void ClickButton()
    {
        click.Play();
    }
}
