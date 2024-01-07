using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CustomUI/SliderSO", fileName = "Slider")]
public class SliderSO : ScriptableObject
{
    public bool interactible;
    public Color backgroundColor;
    public Color fillColor;
    public float min;
    public float max;
}
