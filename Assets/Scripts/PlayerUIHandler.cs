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
    public GameObject endRaceObject;

    public RacePositionHandler racePositionHolder;

    TextMeshProUGUI velocityText;
    TextMeshProUGUI timeText;
    TextMeshProUGUI lapsText;
    TextMeshProUGUI[] previousLaps;
    TextMeshProUGUI endRacePosition;


    TimeSpan currentTime = new TimeSpan();

    void Start()
    {
        instance = this;

        velocityText = velocityTextObject.GetComponent<TextMeshProUGUI>();

        timeText = timeTextObject.GetComponent<TextMeshProUGUI>();
        lapsText = lapsTextObject.GetComponent<TextMeshProUGUI>();

        endRacePosition = endRaceObject.transform.Find("RacePosition").GetComponent<TextMeshProUGUI>();

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
    public void ShowEndRaceScreen(int playerRacePosition)
    {
        endRaceObject.SetActive(true);

        switch (playerRacePosition)
        {
            case 0:
                endRacePosition.text = "1st place";
                break;
            case 1:
                endRacePosition.text = "2nd place";
                break;
            case 2:
                endRacePosition.text = "3rd place";
                break;
            default:
                endRacePosition.text = playerRacePosition+"th place";
                break;
        }
    }
}
