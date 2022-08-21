using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForPlayerSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = GameObject.Find("Player").GetComponent<Player>().healValue;
        slider.value = slider.maxValue;
    }

}