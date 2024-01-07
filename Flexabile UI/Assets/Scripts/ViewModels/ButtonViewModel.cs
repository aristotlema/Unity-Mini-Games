using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonViewModel : MonoBehaviour
{
    public CustomButton button;

    [Header("Event")]
    public GameEvent onClick;
    
    [Header("Event Data")]
    public int value;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        onClick.Raise(this, value);
    }
}
