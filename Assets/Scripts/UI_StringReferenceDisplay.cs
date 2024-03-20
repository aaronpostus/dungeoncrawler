using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StringReferenceDisplay : MonoBehaviour
{
    [SerializeField] StringReference String;
    private TextMeshProUGUI text;
    private void Awake() { text = GetComponent<TextMeshProUGUI>(); }

    private void OnEnable() { String.OnChange += UpdateText; }

    private void OnDisable() { String.OnChange -= UpdateText; }

    private void UpdateText() { text.text = String.Value; }
}
