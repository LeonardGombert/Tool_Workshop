using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    protected InputAction movementAcion;
    protected InputMapping customBindings;
    public MovementData movementData;
    Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        customBindings = new InputMapping();

        movementAcion = customBindings.Player.Move;
        movementAcion.performed += ctx => Move(ctx);
        movementAcion.canceled += ctx => Move(ctx);
    }

    void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        movementData.direction = direction;
    }

    private void OnEnable()
    {
        customBindings.Enable();
    }

    private void OnDisable()
    {
        customBindings.Disable();
    }
}
