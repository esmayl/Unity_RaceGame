using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
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
        PlayerUIHandler.instance.UpdateCurrentLap(currentLap, amountOfLaps);
        finish.GetComponent<TriggerHandler>().triggerDelegate += LapDelegate;
        midPoint.GetComponent<TriggerHandler>().triggerDelegate += PastMidpoint;

        midPointPast.Add(false);
        laptimes.Add(0, new List<float>());
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
            gameTime = 0;
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
            currentLap++;
            PlayerUIHandler.instance.UpdateCurrentLap(currentLap, amountOfLaps);
        }
        else
        {
            Debug.Log("Race over!");
        }
    }
}
