using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject car;
    public float followSpeed;

    float heightOffset;
    Vector3 offset;

    void Start()
    {
        heightOffset = (car.transform.position - transform.position).y;
    }

    void Update()
    {
        offset = car.transform.forward * 4;
        offset.y = heightOffset;

        transform.eulerAngles = car.transform.eulerAngles;
        transform.position = Vector3.Lerp(transform.position,car.transform.position - offset,Time.deltaTime*followSpeed);
    }
}
