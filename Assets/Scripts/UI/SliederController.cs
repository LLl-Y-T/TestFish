using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliederController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public void UpdateSlider(float value)
    {
        if(_slider.maxValue >= value)
            _slider.value = value;
        else
            _slider.value = _slider.maxValue;
    }

    public void UpdateMaxValue(float maxValue)
    {
        if(maxValue > 0) 
            _slider.maxValue = maxValue;
    }
}
