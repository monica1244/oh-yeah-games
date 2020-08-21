using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void setMaxValue(int val) {
        slider.maxValue = val;
        slider.value = val;
    }

    public void setValue(int val) {
        slider.value = val;
    }
}
