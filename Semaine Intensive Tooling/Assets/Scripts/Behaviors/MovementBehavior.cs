using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.WSA.Input;

namespace Gameplay.Player
{
    public class MovementBehavior : MovementData
    {
        private InputMapping inputMapping;
        private InputAction movementAcion;

        // Start is called before the first frame update
        void Awake()
        {
            inputMapping = new InputMapping();

            movementAcion = inputMapping.Player.Move;
            movementAcion.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            movementAcion.canceled += ctx => moveDirection = Vector3.zero;

            camera = Camera.main; // get reference with editor

            // draw these in editor with Handles
            constraints1 = new Vector3(0 + 100, 0 + 100);
            constraints2 = new Vector3(camera.pixelWidth - 100, camera.pixelHeight - 100);
        }

        void Start()
        {
            StartCoroutine(MoveShip());
        }

        IEnumerator MoveShip()
        {
            while (true)
            {
                var movement = moveDirection * movementSpeed * Time.deltaTime;

                Vector3 targetPos = camera.WorldToScreenPoint(transform.position + movement);

                if (targetPos.x > constraints1.x && targetPos.y > constraints1.y &&
                    targetPos.x < constraints2.x && targetPos.y < constraints2.y)
                    transform.position += movement;

                yield return null;
            }
        }

        #region Input System
        private void OnEnable()
        {
            inputMapping.Enable();
        }

        private void OnDisable()
        {
            inputMapping.Disable();
        }
        #endregion
    }
}