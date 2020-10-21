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

                Vector3 targetPos = Camera.main.WorldToScreenPoint(transform.position + movement);

                if (targetPos.x > playSpace.leftX && targetPos.y > playSpace.leftY &&
                    targetPos.x < playSpace.rightX && targetPos.y < playSpace.rightY)
                    transform.position += movement;

                screenSpacePosition = transform.position;

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