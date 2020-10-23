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
                if (currentPos.x < playspace.leftX && currentPos.y > playspace.rightY || currentPos.x > playspace.rightX && currentPos.y < playspace.leftY)
                {
                    if (currentPos.x < playspace.leftX && currentPos.y > playspace.rightY)
                        transform.position = new Vector3(transform.position.x + transitionSpeed * Time.deltaTime, transform.position.y - transitionSpeed * Time.deltaTime, transform.position.z);
                    if (currentPos.x > playspace.rightX && currentPos.y < playspace.leftY)
                        transform.position = new Vector3(transform.position.x - transitionSpeed * Time.deltaTime, transform.position.y + transitionSpeed * Time.deltaTime, transform.position.z);
                }

                // CARDINAL MOVEMENT
                else
                {
                    if (currentPos.x < playspace.leftX) transform.position = new Vector3(transform.position.x + transitionSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    else if (currentPos.x > playspace.rightX) transform.position = new Vector3(transform.position.x - transitionSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    if (currentPos.y < playspace.leftY) transform.position = new Vector3(transform.position.x, transform.position.y + transitionSpeed * Time.deltaTime, transform.position.z);
                    else if (currentPos.y > playspace.rightY) transform.position = new Vector3(transform.position.x, transform.position.y - transitionSpeed * Time.deltaTime, transform.position.z);
                }

                // PLAYSPACE CONSTRAINTS
                if (targetPos.x > playspace.leftX && targetPos.y > playspace.leftY &&
                    targetPos.x < playspace.rightX && targetPos.y < playspace.rightY)
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