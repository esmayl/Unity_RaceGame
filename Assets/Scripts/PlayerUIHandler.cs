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
    public GameObject lapsTextObject;


    TextMeshProUGUI velocityText;
    TextMeshProUGUI timeText;
    TextMeshProUGUI lapsText;


    TimeSpan currentTime = new TimeSpan();

    void Awake()
    {
        instance = this;

        velocityText = velocityTextObject.GetComponent<TextMeshProUGUI>();
        timeText = timeTextObject.GetComponent<TextMeshProUGUI>();

        lapsText = lapsTextObject.GetComponent<TextMeshProUGUI>();
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

    public void UpdateCurrentLap(int currentLap,int amountOfLaps)
    {
        lapsText.text = currentLap+"/"+amountOfLaps;
    }
}
