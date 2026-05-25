using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private rb playerRB;
    private int previousValue;

    void Start()
    {
        slider.onValueChanged.AddListener((value) => {
            int index = (int)value;
            if (index < (int)slider.maxValue)
                previousValue = index;
            playerRB.SetAccelerationByIndex(index);
        });
    }

    void Update()
    {
        if ((int)slider.value == (int)slider.maxValue 
            && !Input.GetMouseButton(0)) {
                slider.value = previousValue;
        }
    }
}
