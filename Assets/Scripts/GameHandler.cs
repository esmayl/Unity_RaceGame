using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public GameObject finish;
    public GameObject midPoint;
    public GameObject checkpoints;

    public int amountOfLaps;
    public int amountOfPlayers = 2;
    int[] currentLaps;
    float[] gameTimers;

    List<bool> midPointPast = new List<bool>();

    Dictionary<int, List<float>> laptimes = new Dictionary<int, List<float>>();
    List<CarController> players = new List<CarController>();
    int[] racePositions;
    int[] checkpointCounter;
    int[] currentCheckpoint;
    Checkpoint[] allCheckpoints;

    void Awake()
    {
        finish.GetComponent<TriggerHandler>().triggerDelegate += LapDelegate;
        midPoint.GetComponent<TriggerHandler>().triggerDelegate += PastMidpoint;

        int i = 0;
        foreach(Transform t in checkpoints.transform)
        {
            Checkpoint temp = t.GetComponent<Checkpoint>();
            temp.checkpointId = i;
            temp.triggerDelegate += CheckpointDelegate;
            temp.location = temp.transform.position;

            i++;
        }
        checkpointCounter = new int[amountOfPlayers];
        currentCheckpoint = new int[amountOfPlayers];

        allCheckpoints = new Checkpoint[checkpoints.transform.childCount];

        instance = this;

        gameTimers = new float[amountOfPlayers];
        currentLaps = new int[amountOfPlayers];
        racePositions = new int[amountOfPlayers];

        List<float> tempTimes = new List<float>();

        for (i = 0; i < amountOfPlayers; i++)
        {
            midPointPast.Add(false);
            laptimes.Add(i, new List<float>() { 0f });
            currentLaps[i] = 0;
            racePositions[i] = i;
        }

        i = 0;
        foreach (Transform child in checkpoints.transform)
        {
            allCheckpoints[i] = child.GetComponent<Checkpoint>();
            i++;
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

            laptimes[i][currentLaps[i]] = gameTimers[i];
        }

        PlayerUIHandler.instance.UpdateTimeText(gameTimers[0]);

        for (int i = 0; i < amountOfPlayers; i++)
        {
            PlayerUIHandler.instance.racePositionHolder.UpdateTime(racePositions[i], i, laptimes[i][currentLaps[i]]);
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
            if (laptimes[playerId].Count < 1)
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

    public void NextLap(int playerId)
    {
        if (currentLaps[playerId] < amountOfLaps)
        {
            currentLaps[playerId]++;
            
            if (playerId == 0)
            {
                PlayerUIHandler.instance.UpdatePreviousTimeText(gameTimers[playerId], currentLaps[playerId]);
                PlayerUIHandler.instance.UpdateCurrentLap(currentLaps[playerId]+1, amountOfLaps);
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

    void SetRacePositions()
    {

        for (int j = 0; j < amountOfPlayers; j++)
        {
            if(currentLaps[j] == amountOfLaps) { continue; }

            int highest = checkpointCounter[j];
            int index = 0;

            for (int i = 0; i < amountOfPlayers; i++)
            {
                if(i == j) { continue; }
                     
                if(checkpointCounter[i] == highest)
                {
                    float distanceJ = Vector3.Distance(allCheckpoints[currentCheckpoint[j]].location, players[j].transform.position);
                    float distanceI = Vector3.Distance(allCheckpoints[currentCheckpoint[i]].location, players[i].transform.position);

                    if(distanceJ < distanceI)
                    {
                        index++;
                    }
                }

                if (checkpointCounter[i] > highest)
                {
                    index++;
                }
            }
            racePositions[j] = index;
        }


    }

    public void CheckpointDelegate(int playerId,int checkpointId)
    {
        checkpointCounter[playerId]++;
        currentCheckpoint[playerId] = checkpointId;

        SetRacePositions();
    }

}
