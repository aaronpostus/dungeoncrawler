using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSettings : MonoBehaviour
{

    [SerializeField] private TMP_Text masterTextValue = null;
    [SerializeField] private Slider masterVolumeSlider = null;

    [SerializeField] private TMP_Text musicTextValue = null;
    [SerializeField] private Slider musicVolumeSlider = null;

    [SerializeField] private TMP_Text soundEffectsTextValue = null;
    [SerializeField] private Slider soundEffectsSlider = null;

    [SerializeField] private GameObject confirmationPrompt;
    [SerializeField] private GameObject resetPrompt;

    private float defaultValue = 50f;

    public void SetMasterVolume(float volume)
    {
        //change volume in game with FMOD?
        AudioListener.volume = volume / 100;

        masterTextValue.text = volume.ToString("0");
    }

    public void SetMusicVolume(float volume)
    {
        //change volume in game with FMOD?

        musicTextValue.text = volume.ToString("0");
    }

    public void SetSoundEffectsVolume(float volume)
    {
        //change volume in game with FMOD?

        soundEffectsTextValue.text = volume.ToString("0");
    }


    public void ResetButtonClicked()
    {
        //resets master volume
        AudioListener.volume = defaultValue / 100;
        masterTextValue.text = defaultValue.ToString("0");
        masterVolumeSlider.value = defaultValue;

        //resets music volume
        musicTextValue.text = defaultValue.ToString("0");
        musicVolumeSlider.value = defaultValue;

        //resets sound effect volume
        soundEffectsTextValue.text = defaultValue.ToString("0");
        soundEffectsSlider.value = defaultValue;

        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ResetBox());
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);

        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public IEnumerator ResetBox()
    {
        resetPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        resetPrompt.SetActive(false);
    }

}
