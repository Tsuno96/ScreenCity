using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueTotext : MonoBehaviour
{
    Slider sliderUI;
    Text textSliderValue;
    void Awake()
    {
        textSliderValue = GetComponent<Text>();
        sliderUI = GetComponentInParent<Slider>();
        ShowSliderValue();
    }
    

    public void ShowSliderValue()
    {
        string sliderMessage = (sliderUI.value*100).ToString("f1")+"%";
        textSliderValue.text = sliderMessage;
    }
}
