using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextField : MonoBehaviour
{
    public TextFieldSO textData;
    public Style style;

    private TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Setup();
        Configure();
    }

    private void Setup()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Configure()
    {
        textMeshProUGUI.color = textData.theme.GetTextColor(style);
        textMeshProUGUI.font = textData.font;
        textMeshProUGUI.fontSize = textData.size;
    }


    private void OnValidate()
    {
        Init();
    }
}
