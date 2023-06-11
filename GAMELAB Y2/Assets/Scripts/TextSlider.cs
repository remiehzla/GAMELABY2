using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.TextMeshProUGUI;
using System;
using TMPro;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        SetNumberText();
    }

    private void SetNumberText()
    {
        throw new NotImplementedException();
    }

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}