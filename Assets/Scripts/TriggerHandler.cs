using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public delegate void TriggerDelegate(int playerId);
    public event TriggerDelegate triggerDelegate;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            triggerDelegate(other.GetComponent<CarController>().playerId);
        }
    }
}
