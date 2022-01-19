using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler instance;

    public GameObject velocityTextObject;
    public GameObject timeTextObject;

    TextMeshProUGUI velocityText;
    TextMeshProUGUI timeText;

    TimeSpan currentTime = new TimeSpan();

    void Awake()
    {
        instance = this;

        velocityText = velocityTextObject.GetComponent<TextMeshProUGUI>();
        timeText = timeTextObject.GetComponent<TextMeshProUGUI>();
    }


    public void UpdateVelocityText(float newVelocity)
    {
        velocityText.text = (int)newVelocity+"";
    }

    public void UpdateTimeText(float newTime)
    {
        currentTime = TimeSpan.FromSeconds(newTime);

        timeText.text = currentTime.ToString(@"mm\:ss\:fff");    
    }
}
