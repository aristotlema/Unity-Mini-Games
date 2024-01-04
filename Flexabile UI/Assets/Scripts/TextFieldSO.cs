using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "CustomUI/TextFieldSO", fileName = "Text")]
public class TextFieldSO : ScriptableObject
{
    public ThemeSO theme;

    public TMP_FontAsset font;
    public float size;
}
