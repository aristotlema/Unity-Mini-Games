using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ThemeSO", fileName = "Theme")]
public class ThemeSO : ScriptableObject
{
    [Header("Primary")]
    public Color primary_bg;
    public Color primary_text;

    [Header("Secondary")]
    public Color secondary_bg;
    public Color secondary_text;

    [Header("Tertiary")]
    public Color tertiary_bg;
    public Color tertiary_text;

    [Header("Other")]
    public Color disable;
}
