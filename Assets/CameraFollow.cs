using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject car;
    public float followSpeed;

    Vector3 offset;

    void Start()
    {
        offset = car.transform.position - transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,car.transform.position - offset,Time.deltaTime*followSpeed);

        transform.LookAt(car.transform);
    }
}
