using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider slider;

    // int for energy
    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }

    // float for time
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetValue(float value)
    {
        slider.value = value;
    }
}
