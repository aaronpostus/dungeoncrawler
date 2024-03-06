using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

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


    private FMOD.Studio.Bus Master;
    private FMOD.Studio.Bus Music;
    private FMOD.Studio.Bus SFX;
    private float MusicVolume;
    private float SFXVolume;
    private float MasterVolume;

    private float defaultValue = 0.5f;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        MusicVolume = defaultValue;
        SFXVolume = defaultValue;
        MasterVolume = 1f;
    }

    public void SetMasterVolume(float newMasterVolume)
    {
        MasterVolume = newMasterVolume / 100f;

        masterTextValue.text = newMasterVolume.ToString("0");
    }

    public void SetMusicVolume(float newMusicVolume)
    {
        MusicVolume = newMusicVolume / 100f;

        musicTextValue.text = newMusicVolume.ToString("0");
    }

    public void SetSoundEffectsVolume(float newSFXVolume)
    {
        SFXVolume = newSFXVolume / 100f;

        soundEffectsTextValue.text = newSFXVolume.ToString("0");
    }

    public void ResetButtonClicked()
    {
        //resets master volume
        Master.setVolume(defaultValue);
        masterTextValue.text = (defaultValue * 100).ToString("0");
        masterVolumeSlider.value = (defaultValue * 100);

        //resets music volume
        Music.setVolume(defaultValue);
        musicTextValue.text = (defaultValue * 100).ToString("0");
        musicVolumeSlider.value = (defaultValue * 100);

        //resets sound effect volume
        SFX.setVolume(defaultValue);
        soundEffectsTextValue.text = (defaultValue * 100).ToString("0");
        soundEffectsSlider.value = (defaultValue * 100);

        PlayerPrefs.SetFloat("masterVolume", defaultValue);
        PlayerPrefs.SetFloat("musicVolume", defaultValue);
        PlayerPrefs.SetFloat("soundEffectsVolume", defaultValue);

        StartCoroutine(ResetBox());
    }

    public void VolumeApply()
    {
        Master.setVolume(MasterVolume);
        Debug.Log(MasterVolume.ToString());
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);

        PlayerPrefs.SetFloat("masterVolume", MasterVolume);
        PlayerPrefs.SetFloat("musicVolume", MusicVolume);
        PlayerPrefs.SetFloat("soundEffectsVolume", SFXVolume);

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
