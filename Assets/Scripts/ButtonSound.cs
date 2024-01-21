using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        audioSource.clip = clip;
    }
    
    public void playClip()
    {
        audioSource.Play();
    }
}
