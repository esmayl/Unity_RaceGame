using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler instance;

    public GameObject raceStartCounterObject;
    public GameObject velocityTextObject;
    public GameObject currentTimeTextObject;
    public GameObject lapsTextObject;
    public GameObject previousLapsHolder;
    public GameObject previousLapsTemplate;
    public GameObject endRaceObject;

    public RacePositionHandler racePositionHolder;

    TextMeshProUGUI raceStartCounterText;
    TextMeshProUGUI velocityText;
    TextMeshProUGUI currentTimeText;
    TextMeshProUGUI lapsText;
    TextMeshProUGUI[] previousLaps;
    TextMeshProUGUI endRacePosition;


    TimeSpan currentTime = new TimeSpan();

    void Start()
    {
        instance = this;

        raceStartCounterText = raceStartCounterObject.GetComponent<TextMeshProUGUI>();
        velocityText = velocityTextObject.GetComponent<TextMeshProUGUI>();
        currentTimeText = currentTimeTextObject.GetComponent<TextMeshProUGUI>();
        lapsText = lapsTextObject.GetComponent<TextMeshProUGUI>();

        endRacePosition = endRaceObject.transform.Find("RacePosition").GetComponent<TextMeshProUGUI>();

        previousLaps = new TextMeshProUGUI[GameHandler.instance.amountOfLaps];



        UpdateCurrentLap(0, GameHandler.instance.amountOfLaps);


        for (int i = 0; i < GameHandler.instance.amountOfLaps; i++)
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

        currentTimeText.text = currentTime.ToString(@"mm\:ss\:fff");    
    }

    public void UpdateCurrentLap(int currentLap,int amountOfLaps)
    {
        lapsText.text = (currentLap + 1)+"/"+amountOfLaps;
    }

    public void UpdatePreviousTimeText(float newTime,int currentLap)
    {
        currentTime = TimeSpan.FromSeconds(newTime);

        previousLaps[currentLap-1].text = currentTime.ToString(@"mm\:ss\:fff");
    }
    public void ShowEndRaceScreen(int playerRacePosition)
    {
        endRaceObject.SetActive(true);

        switch (playerRacePosition)
        {
            case 0:
                endRacePosition.text = "1st place";
                return;
            case 1:
                endRacePosition.text = "2nd place";
                return;
            case 2:
                endRacePosition.text = "3rd place";
                return;
        }

        endRacePosition.text = (playerRacePosition + 1) + "th place";

    }

    public void ShowStartTimer(int count)
    {
        raceStartCounterObject.SetActive(true);

        raceStartCounterText.text = ""+count;
        raceStartCounterObject.GetComponent<Animator>().SetTrigger("Show");
    }

    public void DisableStartTimer()
    {
        raceStartCounterObject.SetActive(false);
    }
}
