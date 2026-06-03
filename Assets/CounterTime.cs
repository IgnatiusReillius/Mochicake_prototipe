using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterTime : MonoBehaviour
{
    [SerializeField] private Text counterText;
    [SerializeField] private int requiredTimeLevel;
    [SerializeField] private FinishLine finishLine;

    void Update()
    {
        float elapsed = GameManager.Instance.ElapsedTime;
        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);
        
        int requiredMin = Mathf.FloorToInt(requiredTimeLevel / 60f);
        int requiredSec = Mathf.FloorToInt(requiredTimeLevel % 60f);

        counterText.text = string.Format("{0:00}:{1:00}", minutes, seconds) + " / " + string.Format("{0:00}:{1:00}", requiredMin, requiredSec);

        if(elapsed > requiredTimeLevel) {
            finishLine.outOfTime();
        }
    }
}
