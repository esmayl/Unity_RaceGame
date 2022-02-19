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
        for (int i = amountOfPlayers; i < racePositionHolder.childCount; i++)
        {
            racePositionHolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void UpdateTime(int position,int playerId,float time)
    {
        TimeSpan currentTime = TimeSpan.FromSeconds(time);
        racePositionHolder.GetChild(position).GetComponent<TextMeshProUGUI>().text = "Player "+(playerId+1) + " - " + currentTime.ToString(@"mm\:ss\:fff");
    }
}
