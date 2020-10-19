using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class MovementBehavior : MovementData
    {
        private void Awake()
        {
            customBindings = new InputMapping();

            movementAcion = customBindings.Player.Move;
            movementAcion.performed += ctx => Move(ctx);
            movementAcion.canceled += ctx => Move(ctx);

            camera = Camera.main;

            constraints1 = new Vector3(0 + 100, 0 + 100);
            constraints2 = new Vector3(camera.pixelWidth - 100, camera.pixelHeight - 100);
    }

        void Move(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            customBindings.Enable();   
        }

        private void Update()
        {
            var movement = direction * movementSpeed * Time.deltaTime;
            Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
            Vector3 shiftedPos = camera.WorldToScreenPoint(transform.position + movement);
            Debug.Log("target is " + screenPos.x + " pixels from the left");

            if (shiftedPos.x > constraints1.x && shiftedPos.y > constraints1.y &&
                shiftedPos.x < constraints2.x && shiftedPos.y < constraints2.y)
                transform.position += movement;
        }

        private void OnDisable()
        {
            customBindings.Disable();
        }
    }
}