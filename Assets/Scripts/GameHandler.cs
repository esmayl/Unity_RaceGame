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

    void Awake()
    {
        finish.GetComponent<TriggerHandler>().triggerDelegate += LapDelegate;
        midPoint.GetComponent<TriggerHandler>().triggerDelegate += PastMidpoint;

        instance = this;

        gameTimers = new float[amountOfPlayers];
        currentLaps = new int[amountOfLaps];

        for (int i = 0; i < amountOfPlayers; i++)
        {
            midPointPast.Add(false);
            laptimes.Add(i, new List<float>());
        }

    }


    void Start()
    {
        PlayerUIHandler.instance.racePositionHolder.InitRacePositionList(amountOfPlayers);
    }

    void PastMidpoint(int playerId)
    {
        midPointPast[playerId] = true;
    }

    void LapDelegate(int playerId)
    {
        if (midPointPast[playerId])
        {
            laptimes[playerId].Add(gameTimers[playerId]);
            midPointPast[playerId] = false;
            NextLap(playerId);
        }

    }

    void Update()
    {
        for (int i = 0; i < gameTimers.Length; i++) 
        {
            gameTimers[i] += Time.deltaTime;
        }

        PlayerUIHandler.instance.UpdateTimeText(gameTimers[0]);
    }


    public void NextLap(int playerId)
    {
        if (currentLaps[playerId] < amountOfLaps)
        {

            PlayerUIHandler.instance.racePositionHolder.UpdateTime(playerId, laptimes[playerId][currentLaps[playerId]]);

            currentLaps[playerId]++;
            
            if (playerId == 0)
            {
                PlayerUIHandler.instance.UpdatePreviousTimeText(gameTimers[playerId], currentLaps[playerId]);
                PlayerUIHandler.instance.UpdateCurrentLap(currentLaps[playerId], amountOfLaps);
            }

            gameTimers[playerId] = 0;
        }
        else
        {
            Debug.Log("Race over!");
        }
    }
}
