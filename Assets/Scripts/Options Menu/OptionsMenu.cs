using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionButtons;

    [SerializeField] private GameObject optionTemplate;

    [SerializeField] private GameObject soundOptions;

    private GameObject currentOption;

    public void OnGraphicsClicked()
    {
     
    }

    public void OnSoundClicked()
    {
        currentOption = soundOptions;

        optionButtons.SetActive(false);

        optionTemplate.SetActive(true);
        currentOption.SetActive(true);
    }

    public void OnAccessbilityClicked()
    {

    }

    public void onCurrentOptionReturn()
    {
        optionTemplate.SetActive(false);
        currentOption.SetActive(false);
        
        optionButtons.SetActive(true);
    }
}
