using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const float _hoursToDegress = -30f;
    private const float _minutesToDegress = -6f;
    private const float _secondsToDegress = -6f;

    [SerializeField] Transform hoursPivot, minutesPivot, secondsPivot;

    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;

        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, _hoursToDegress * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, _minutesToDegress * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, _secondsToDegress * (float)time.TotalSeconds);
    }
}
