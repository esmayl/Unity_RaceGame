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
    public GameObject previousLapsHolder;
    public GameObject previousLapsTemplate;

    TextMeshProUGUI velocityText;
    TextMeshProUGUI timeText;
    TextMeshProUGUI lapsText;
    TextMeshProUGUI[] previousLaps;


    TimeSpan currentTime = new TimeSpan();

    void Start()
    {
        instance = this;

        velocityText = velocityTextObject.GetComponent<TextMeshProUGUI>();
        timeText = timeTextObject.GetComponent<TextMeshProUGUI>();

        lapsText = lapsTextObject.GetComponent<TextMeshProUGUI>();

        int tempLaps = GameHandler.instance.amountOfLaps;
        previousLaps = new TextMeshProUGUI[tempLaps];

        
        UpdateCurrentLap(1, tempLaps);


        for (int i = 0; i < tempLaps; i++)
        {
            GameObject temp = Instantiate(previousLapsTemplate, previousLapsHolder.transform);
            previousLaps[i] = temp.GetComponent<TextMeshProUGUI>();
            previousLaps[i].text = "";
        }
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

    public void UpdatePreviousTimeText(float newTime,int currentLap)
    {
        currentTime = TimeSpan.FromSeconds(newTime);

        previousLaps[currentLap - 1].text = currentTime.ToString(@"mm\:ss\:fff");
    }
}
