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

        for (int i = 0; i < amountOfPlayers; i++)
        {
            racePositionHolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = (i+1)+"";
        }
    }

    public void UpdateTime(int position,int playerId,float time)
    {
        TimeSpan currentTime = TimeSpan.FromSeconds(time);
        racePositionHolder.GetChild(position).GetComponent<TextMeshProUGUI>().text = (position+1)+" Player "+(playerId+1) + " - "+currentTime.ToString(@"mm\:ss\:fff");
    }
}
