using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public int amountOfLaps;
    int currentLap;

    float gameTime;

    void Awake()
    {
        currentLap = 1;
        PlayerUIHandler.instance.UpdateCurrentLap(currentLap, amountOfLaps);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        PlayerUIHandler.instance.UpdateTimeText(gameTime);
    }


    public void NextLap()
    {
        if (currentLap < amountOfLaps)
        {
            currentLap++;
            PlayerUIHandler.instance.UpdateCurrentLap(currentLap, amountOfLaps);
        }
        else
        {
            Debug.Log("Race over!");
        }
    }
}
