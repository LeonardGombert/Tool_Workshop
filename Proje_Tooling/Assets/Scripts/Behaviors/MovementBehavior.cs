using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
            playSpace.leftX = 100;
            playSpace.leftY = 100;
            playSpace.rightX = camera.pixelWidth - 100;
            playSpace.rightY = camera.pixelHeight - 100;
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

                if (targetPos.x > playSpace.leftX && targetPos.y > playSpace.leftY &&
                    targetPos.x < playSpace.rightX && targetPos.y < playSpace.rightY)
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