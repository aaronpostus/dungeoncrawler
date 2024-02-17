using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void OnGraphicsClicked()
    {

    }

    public void OnSoundClicked()
    {

    }

    public void OnAccessbilityClicked()
    {

    }

    public void OnReturnClicked()
    {
        MainMenu.instance.Return();
    }
}
