using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointId;
    public Vector3 location;

    public event Delegates.CheckpointDelegate triggerDelegate;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            triggerDelegate(other.GetComponent<CarController>().playerId,checkpointId);
        }

        if(other.gameObject.name == "Car")
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void EnableCoin()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
