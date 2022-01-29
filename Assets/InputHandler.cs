using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public InputAction movementInput;
    CarController controller;

    void Awake()
    {
        movementInput.Enable();
        movementInput.started += MovementInput_performed;
        movementInput.performed += MovementInput_performed;
        movementInput.canceled += MovementInput_performed;

        controller = GetComponent<CarController>();
    }

    private void MovementInput_performed(InputAction.CallbackContext obj)
    {
        controller.playerInput = obj.ReadValue<Vector2>();
    }
}
