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
            racePositions[i] = 0;
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

        SetRacePositions();

        for (int i = 0; i < racePositions.Length; i++)
        {
            PlayerUIHandler.instance.racePositionHolder.UpdateTime(racePositions[i], i, laptimes[i][currentLaps[i] - 1]);
        }


    }

    void OnDrawGizmos()
    {
        //for (int j = 0; j < amountOfPlayers; j++)
        //{

        //    Vector3[] temp = SetRacePositions(j);
        //    foreach (Vector3 item in temp)
        //    {

        //        if (j == 0)
        //        {
        //            Debug.DrawRay(players[j].transform.position, item, Color.red);
        //        }
        //        if (j == 1)
        //        {
        //            Debug.DrawRay(players[j].transform.position, item, Color.blue);
        //        }
        //        if (j == 2)
        //        {
        //            Debug.DrawRay(players[j].transform.position, item, Color.green);
        //        }
        //    }
        //    temp = null;
        //}

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

    void SetRacePositions()
    {
        int positionFound = 0;

        for (int j = 0; j < amountOfPlayers; j++)
        {
            Vector3 comparePos = players[j].transform.position;
            
            positionFound = 0;

            for (int i = 0; i < amountOfPlayers; i++)
            {
                if(i == j) { continue; }


                if (Vector3.Dot(players[j].carForward, players[i].carForward) > 0.7f) // Check if looking in the same direction, to prevent Dot product working on car driving in oposite direction
                {
                    Vector3 dir = players[i].transform.position - comparePos;

                    float dotProduct = Vector3.Dot(players[j].carForward, dir.normalized);

                    if(currentLaps[j] < currentLaps[i])
                    {
                        Debug.Log("Less laps");
                        //if (positionFound < amountOfPlayers - 1)
                        //{
                        //    positionFound++;
                        //}
                    }
                    else if (dotProduct <= 0f && currentLaps[j] == currentLaps[i])
                    {
                        Debug.Log("Dot less");

                        if (positionFound < amountOfPlayers - 1)
                        {
                            positionFound++;
                        }
                    }
                    else if (dotProduct > 0f && currentLaps[j] == currentLaps[i])
                    {
                        Debug.Log("Dot more");

                        if (positionFound > 0)
                        {
                            positionFound--;
                        }
                    }
                }
                else
                {
                    positionFound = racePositions[j];
                }
            }
            racePositions[j] = positionFound;
            
        }


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
