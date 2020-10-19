using UnityEngine;
using UnityEngine.InputSystem;

public class MovementData : MonoBehaviour
{
    protected InputAction movementAcion;
    protected InputMapping customBindings;
    protected Vector3 direction;
    protected float movementSpeed = 22f;

    protected Camera camera;
    protected Vector3 constraints1;
    protected Vector3 constraints2;
}
