using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public GameObject finish;
    public GameObject midPoint;

    public int amountOfLaps;
    public int amountOfPlayers = 2;
    int[] currentLaps;
    float[] gameTimers;

    List<bool> midPointPast = new List<bool>();

    Dictionary<int, List<float>> laptimes = new Dictionary<int, List<float>>();
    List<CarController> players = new List<CarController>();

    void Awake()
    {
        finish.GetComponent<TriggerHandler>().triggerDelegate += LapDelegate;
        midPoint.GetComponent<TriggerHandler>().triggerDelegate += PastMidpoint;

        instance = this;

        gameTimers = new float[amountOfPlayers];
        currentLaps = new int[amountOfLaps];

        List<float> tempTimes = new List<float>();

        for (int i = 0; i < amountOfPlayers; i++)
        {
            midPointPast.Add(false);
            laptimes.Add(i, new List<float>() { 0f });
            currentLaps[i] = 1;
        }

    }


    void Start()
    {
        PlayerUIHandler.instance.racePositionHolder.InitRacePositionList(amountOfPlayers);
    }

    void Update()
    {
        for (int i = 0; i < gameTimers.Length; i++)
        {
            gameTimers[i] += Time.deltaTime;
            laptimes[i][currentLaps[i] - 1] = gameTimers[i];
        }

        PlayerUIHandler.instance.UpdateTimeText(gameTimers[0]);


    }

    public void RegisterPlayer(CarController player)
    {
        players.Add(player);
    }

    void PastMidpoint(int playerId)
    {
        midPointPast[playerId] = true;
    }

    void LapDelegate(int playerId)
    {
        if (midPointPast[playerId])
        {
            if (currentLaps[playerId] < 1)
            {
                laptimes[playerId][0] = gameTimers[playerId];
            }
            else
            {
                laptimes[playerId].Add(gameTimers[playerId]);
            }
            
            midPointPast[playerId] = false;
            NextLap(playerId);
        }

    }

    int GetRacePosition(int playerId)
    {
        if (!laptimes.ContainsKey(playerId)) { return -1; }

        TimeSpan playerTime = TimeSpan.FromSeconds(laptimes[playerId][laptimes[playerId].Count - 1]);

        int positionFound = 0;

        TimeSpan compareTime = new TimeSpan();

        for (int i = 0; i < amountOfPlayers; i++)
        {
            if(i == playerId) { continue; }

            compareTime = TimeSpan.FromSeconds(laptimes[i][laptimes[i].Count-1]);

            if (compareTime.CompareTo(playerTime) <= 0)
            {
                positionFound = i;
            }
            else
            {
                positionFound++;
            }
        }

        return positionFound;
    }

    public void NextLap(int playerId)
    {
        if (currentLaps[playerId] < amountOfLaps)
        {
            currentLaps[playerId]++;
            
            if (playerId == 0)
            {
                PlayerUIHandler.instance.UpdatePreviousTimeText(gameTimers[playerId], currentLaps[playerId]-1);
                PlayerUIHandler.instance.UpdateCurrentLap(currentLaps[playerId], amountOfLaps);
            }
        }
        else
        {
            if (playerId == 0) 
            {
                PlayerUIHandler.instance.ShowEndRaceScreen(GetRacePosition(playerId));
                Debug.Log("Race over!");
            }
        }

        gameTimers[playerId] = 0;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            int position = GetRacePosition(i);
            if (position != -1)
            {
                PlayerUIHandler.instance.racePositionHolder.UpdateTime(position, i, laptimes[i][laptimes[i].Count - 1]);
            }
        }
    }
}
