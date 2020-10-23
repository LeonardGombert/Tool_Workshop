using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
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

        // called on collision with a playspace change object
        public IEnumerator ChangePlayspace(Vector4Bounds newCoords, int tweenIndex)
        {
            Debug.Log("In the coroutine");
            Vector4Bounds oldValues = playspace;

            // target - start values
            changeLX = newCoords.leftX - oldValues.leftX;
            changeLY = newCoords.leftY - oldValues.leftY;
            changeRX = newCoords.rightX - oldValues.rightX;
            changeRY = newCoords.rightY - oldValues.rightY;
            time = 0f;
            tweenDuration = 1.5f;

            while (time < tweenDuration)
            {
                Debug.Log("Changing values");
                playspace.leftX = TweenManager.tweenFunctions[tweenIndex](time, oldValues.leftX, changeLX, tweenDuration);
                playspace.leftY = TweenManager.tweenFunctions[tweenIndex](time, oldValues.leftY, changeLY, tweenDuration);
                playspace.rightX = TweenManager.tweenFunctions[tweenIndex](time, oldValues.rightX, changeRX, tweenDuration);
                playspace.rightY = TweenManager.tweenFunctions[tweenIndex](time, oldValues.rightY, changeRY, tweenDuration);
                time += Time.deltaTime;
                yield return null;
            }

            rescaled = true;
            Debug.Log("I'm out");
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