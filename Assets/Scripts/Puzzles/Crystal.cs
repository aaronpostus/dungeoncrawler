﻿using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] CrystalType type;
    public enum CrystalType { BLUE, RED, GREEN }
    
    public void Start()
    {
        particles.Stop();
    }
    public CrystalType GetType() {
        return type;
    }
    public void Power() {
        particles.Play();
    }
    public void StopPowering() {
        particles.Stop();
    }

}
