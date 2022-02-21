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
    int[] racePositions;

    void Awake()
    {
        finish.GetComponent<TriggerHandler>().triggerDelegate += LapDelegate;
        midPoint.GetComponent<TriggerHandler>().triggerDelegate += PastMidpoint;

        instance = this;

        gameTimers = new float[amountOfPlayers];
        currentLaps = new int[amountOfPlayers];
        racePositions = new int[amountOfPlayers];

        List<float> tempTimes = new List<float>();

        for (int i = 0; i < amountOfPlayers; i++)
        {
            midPointPast.Add(false);
            laptimes.Add(i, new List<float>() { 0f });
            currentLaps[i] = 1;
        }

        for (int i = 0; i < racePositions.Length; i++)
        {
            racePositions[i] = i;
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

        for (int i = 0; i < amountOfPlayers-1; i++)
        {
            SetRacePositions(i);
        }

        for (int i = 0; i < racePositions.Length; i++)
        {
            PlayerUIHandler.instance.racePositionHolder.UpdateTime(racePositions[i], i, laptimes[i][laptimes[i].Count - 1]);
        }


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

    void SetRacePositions(int playerId)
    {
        int positionFound = racePositions[playerId];

        Vector3 comparePos = players[playerId].transform.position;

        for (int i = 0; i < amountOfPlayers - 1; i++)
        {
            if(i == playerId) { continue; }
                
            if (currentLaps[i] != currentLaps[i + 1]) { continue; } // Prevent position change when lapping other car

            if (Vector3.Distance(comparePos, players[i].transform.position) < 10) // Check if in range, to prevent Dot product working on car driving in oposite direction
            {
                Vector3 dir = comparePos - players[i].transform.position;

                float dotProduct = Vector3.Dot(players[playerId].carForward, dir.normalized);

                //Debug.Log(dotProduct);

                if (dotProduct < -0.6f && racePositions[i] < positionFound) // Check if the car with playerId is behind another car, if so change position
                {
                    Debug.Log(players[i].playerId + " " + dotProduct);
                    if (positionFound < amountOfPlayers - 1)
                    {
                        positionFound++;
                    }
                }
            }
        }

        racePositions[playerId] = positionFound;

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
                PlayerUIHandler.instance.ShowEndRaceScreen(racePositions[playerId]);
                Debug.Log("Race over!");
            }
        }

        gameTimers[playerId] = 0;
    }
}
