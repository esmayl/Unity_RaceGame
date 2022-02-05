using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    AudioSource thisSource;
    CarController thisCar;

    void Awake()
    {
        thisSource = transform.Find("AudioSource").GetComponent<AudioSource>();
        thisCar = GetComponent<CarController>();
    }

    void Update()
    {
        thisSource.pitch = thisCar.velocity * 0.01f;        
    }
}
