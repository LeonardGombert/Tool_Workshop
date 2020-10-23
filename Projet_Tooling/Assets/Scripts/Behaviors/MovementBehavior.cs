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

                Vector3 currentPos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 targetPos = Camera.main.WorldToScreenPoint(transform.position + movement);

                //prepare for some YandereDev-style if else loops, bruh

                // DIAGONAL MOVEMENT
                if (currentPos.x < playSpace.leftX && currentPos.y > playSpace.rightY || currentPos.x > playSpace.rightX && currentPos.y < playSpace.leftY)
                {
                    if (currentPos.x < playSpace.leftX && currentPos.y > playSpace.rightY)
                        transform.position = new Vector3(transform.position.x + transitionSpeed * Time.deltaTime, transform.position.y - transitionSpeed * Time.deltaTime, transform.position.z);
                    if (currentPos.x > playSpace.rightX && currentPos.y < playSpace.leftY)
                        transform.position = new Vector3(transform.position.x - transitionSpeed * Time.deltaTime, transform.position.y + transitionSpeed * Time.deltaTime, transform.position.z);
                }

                // CARDINAL MOVEMENT
                else
                {
                    if (currentPos.x < playSpace.leftX) transform.position = new Vector3(transform.position.x + transitionSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    else if (currentPos.x > playSpace.rightX) transform.position = new Vector3(transform.position.x - transitionSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    if (currentPos.y < playSpace.leftY) transform.position = new Vector3(transform.position.x, transform.position.y + transitionSpeed * Time.deltaTime, transform.position.z);
                    else if (currentPos.y > playSpace.rightY) transform.position = new Vector3(transform.position.x, transform.position.y - transitionSpeed * Time.deltaTime, transform.position.z);
                }

                // PLAYSPACE CONSTRAINTS
                if (targetPos.x > playSpace.leftX && targetPos.y > playSpace.leftY &&
                    targetPos.x < playSpace.rightX && targetPos.y < playSpace.rightY)
                    transform.position += movement;

                // update the screenspace position (USED BY EDITOR WINDOW)
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