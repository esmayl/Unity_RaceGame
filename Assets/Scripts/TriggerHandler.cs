using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public event Delegates.TriggerDelegate triggerDelegate;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            triggerDelegate(other.GetComponent<CarController>().playerId);
        }
    }
}


public static class Delegates
{
    public delegate void TriggerDelegate(int playerId);
    public delegate void CheckpointDelegate(int playerId, int checkpointId);
}
