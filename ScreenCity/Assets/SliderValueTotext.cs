using UnityEngine;
using UnityEngine.UI;

public class SliderValueTotext : MonoBehaviour
{
    private Slider sliderUI;
    private Text textSliderValue;

    private void Awake()
    {
        textSliderValue = GetComponent<Text>();
        sliderUI = GetComponentInParent<Slider>();
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        string sliderMessage = (sliderUI.value * 100).ToString("f1") + "%";
        textSliderValue.text = sliderMessage;
    }
}