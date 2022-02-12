using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RacePositionHandler : MonoBehaviour
{

    [HideInInspector]public Transform racePositionHolder;
    
    void Start()
    {
        racePositionHolder = transform;
    }

    public void InitRacePositionList(int amountOfPlayers)
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            UpdateTime(i, 0);
        }

        for (int i = amountOfPlayers; i < racePositionHolder.childCount; i++)
        {
            racePositionHolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void UpdateTime(int playerId,float time)
    {
        TimeSpan currentTime = TimeSpan.FromSeconds(time);
        racePositionHolder.GetChild(playerId).GetComponent<TextMeshProUGUI>().text = (playerId+1) + " - " + currentTime.ToString(@"mm\:ss\:fff");
    }
}
