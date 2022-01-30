using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public InputAction movementInput;
    public InputAction brakeInput;

    CarController controller;

    void Awake()
    {
        movementInput.Enable();
        movementInput.started += MovementInput_performed;
        movementInput.performed += MovementInput_performed;
        movementInput.canceled += MovementInput_performed;

        brakeInput.Enable();
        brakeInput.started += Brake_performed;
        brakeInput.canceled += Brake_stop;

        controller = GetComponent<CarController>();
    }

    private void MovementInput_performed(InputAction.CallbackContext obj)
    {
        controller.playerInput = obj.ReadValue<Vector2>();
    }

    void Brake_performed(InputAction.CallbackContext obj)
    {
        controller.Brake();
        Debug.Log("Braking");
    }

    void Brake_stop(InputAction.CallbackContext obj)
    {
        controller.StopBraking();
    }
}
