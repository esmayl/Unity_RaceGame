using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public InputAction movementInput;
    public float velocity;

    public WheelCollider steeringLeft, steeringRight;
    public WheelCollider torqueLeft, torqueRight;
    public Transform rearLeft, rearRight, frontLeft, frontRight;

    public Vector2 playerInput;
    public float speed = 5;

    Rigidbody thisBody;
    float second = 0;
    Vector3 previousPos; 

    void Awake()
    {
        movementInput.Enable();
        movementInput.started += MovementInput_performed;
        movementInput.performed += MovementInput_performed;
        movementInput.canceled += MovementInput_performed;

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
    }

    private void MovementInput_performed(InputAction.CallbackContext obj)
    {
        playerInput = obj.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        torqueLeft.motorTorque = playerInput.y * speed;
        torqueRight.motorTorque = playerInput.y * speed;

        steeringLeft.steerAngle = playerInput.x * 30;
        steeringRight.steerAngle = playerInput.x * 30;


        UpdateWheelPos(torqueLeft, rearLeft);
        UpdateWheelPos(torqueRight, rearRight);
        UpdateWheelPos(steeringLeft, frontLeft);
        UpdateWheelPos(steeringRight, frontRight);

        if(second > 0.1f)
        {
            velocity = Vector3.Distance(previousPos, transform.position) * 3600 / 100; // /100 for km/h
            second = 0;
            previousPos = transform.position;
        }
        else
        {
            second += Time.fixedDeltaTime;
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
