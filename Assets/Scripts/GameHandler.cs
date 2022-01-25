using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public GameObject finish;
    public GameObject midPoint;

    public int amountOfLaps;
    int currentLap;

    float gameTime;

    List<bool> midPointPast = new List<bool>();

    Dictionary<int, List<float>> laptimes = new Dictionary<int, List<float>>();

    void Awake()
    {
        currentLap = 1;
        finish.GetComponent<TriggerHandler>().triggerDelegate += LapDelegate;
        midPoint.GetComponent<TriggerHandler>().triggerDelegate += PastMidpoint;

        midPointPast.Add(false);
        laptimes.Add(0, new List<float>());
        instance = this;
    }

    private void PastMidpoint(int playerId)
    {
        midPointPast[playerId] = true;
    }

    private void LapDelegate(int playerId)
    {
        if (midPointPast[playerId])
        {
            laptimes[playerId].Add(gameTime);
            NextLap();
        }

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
            PlayerUIHandler.instance.UpdatePreviousTimeText(gameTime, currentLap);

            currentLap++;
            
            PlayerUIHandler.instance.UpdateCurrentLap(currentLap, amountOfLaps);

            gameTime = 0;
        }
        else
        {
            Debug.Log("Race over!");
        }
    }
}
