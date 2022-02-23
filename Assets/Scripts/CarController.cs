using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float velocity;

    public WheelCollider steeringLeft, steeringRight;
    public WheelCollider torqueLeft, torqueRight;
    public Transform rearLeft, rearRight, frontLeft, frontRight;

    public Vector2 playerInput;// Set externally by controller script
    public float speed = 2000;

    public int playerId;
    public Vector3 carForward;

    Rigidbody thisBody;
    float second = 0;
    Vector3 previousPos;
    float reducedSpeed = 700;
    float normalSpeed;

    void Awake()
    {
        frontLeft = transform.Find("WheelColliderFL").GetChild(0);
        frontRight = transform.Find("WheelColliderFR").GetChild(0);
        rearLeft = transform.Find("WheelColliderRL").GetChild(0);
        rearRight = transform.Find("WheelColliderRR").GetChild(0);

        steeringLeft = transform.Find("WheelColliderFL").GetComponent<WheelCollider>();
        steeringRight = transform.Find("WheelColliderFR").GetComponent<WheelCollider>();

        torqueLeft = transform.Find("WheelColliderRL").GetComponent<WheelCollider>();
        torqueRight = transform.Find("WheelColliderRR").GetComponent<WheelCollider>();

        thisBody = GetComponent<Rigidbody>();

        previousPos = transform.position;

        normalSpeed = speed;

        carForward = transform.forward;
    }

    void Start()
    {
        GameHandler.instance.RegisterPlayer(this);
    }

    void FixedUpdate()
    {
        GroundCheck();

        Accelerate();

        Steer();

        if (second > 0.1f)
        {
            velocity = Vector3.Distance(previousPos, transform.position) * 3600 / 100; // /100 for km/h
            second = 0;
            previousPos = transform.position;
        }
        else
        {
            second += Time.fixedDeltaTime;
        }

        carForward = transform.forward;
    }


    void Accelerate()
    {
        torqueLeft.motorTorque = playerInput.y * speed;
        torqueRight.motorTorque = playerInput.y * speed;
    }

    void Steer()
    {
        steeringLeft.steerAngle = playerInput.x * 30;
        steeringRight.steerAngle = playerInput.x * 30;


        UpdateWheelPos(torqueLeft, rearLeft);
        UpdateWheelPos(torqueRight, rearRight);
        UpdateWheelPos(steeringLeft, frontLeft);
        UpdateWheelPos(steeringRight, frontRight);
    }

    //Used externally by controller script
    public void Brake()
    {
        torqueLeft.brakeTorque = 1000;
        torqueRight.brakeTorque = 1000;
        steeringLeft.brakeTorque = 1000;
        steeringRight.brakeTorque = 1000;
    }

    //Used externally by controller script
    public void StopBraking()
    {
        torqueLeft.brakeTorque = 0;
        torqueRight.brakeTorque = 0;
        steeringLeft.brakeTorque = 0;
        steeringRight.brakeTorque = 0;
    }

    void GroundCheck()
    {
        if (!Physics.Raycast(transform.position + transform.up + (transform.forward * 2), -transform.up, 10, 1 << LayerMask.NameToLayer("Track")))
        {
            speed = reducedSpeed;
        }
        else if (!Physics.Raycast(transform.position + transform.up + (transform.right * 2), -transform.up, 10, 1 << LayerMask.NameToLayer("Track")))
        {
            speed = reducedSpeed;
        }
        else if (!Physics.Raycast(transform.position + transform.up + (transform.right * -2), -transform.up, 10, 1 << LayerMask.NameToLayer("Track")))
        {
            speed = reducedSpeed;
        }
        else if (!Physics.Raycast(transform.position + transform.up + (transform.forward * -2), -transform.up, 10, 1 << LayerMask.NameToLayer("Track")))
        {
            speed = reducedSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
    }

    void UpdateWheelPos(WheelCollider col,Transform colTransform)
    {
        Vector3 pos = colTransform.position;
        Quaternion rot = colTransform.rotation;

        col.GetWorldPose(out pos, out rot);

        colTransform.position = pos;
        colTransform.rotation = rot;
    }


}
