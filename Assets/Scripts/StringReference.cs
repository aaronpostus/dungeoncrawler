using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringReference", menuName = "ScriptableObjects/StringReference")]
public class StringReference : ScriptableObject
{

    public event Action OnChange;

    public string Value
    {
        get { return value; }
        set
        {
            this.value = value;
            OnChange?.Invoke();
        }
    }

    private string value;
}