using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = GameObject.Find("True Boss").GetComponent<BossAIMover>().healValue;
        slider.value = slider.maxValue;
    }

}
