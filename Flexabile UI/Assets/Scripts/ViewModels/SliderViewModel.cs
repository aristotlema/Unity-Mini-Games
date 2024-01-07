using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderViewModel : MonoBehaviour
{
    public CustomSlider slider;

    public void OnValueChanged(Component sender, object data)
    {
        if (data is int intValue)
            slider.SetValue(intValue);
        else if (data is float floatValue)
            slider.SetValue(floatValue);
    }
}
