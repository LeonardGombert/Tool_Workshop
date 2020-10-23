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

        IEnumerator PlayspaceChangeTransition()
        {
            Vector3 centerOfScreen = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            transform.position = new Vector3(MoveValue((int)transform.position.x), MoveValue((int)transform.position.y), transform.position.z);

            yield return null;
        }

        int MoveValue(int exampleValue)
        {
            change = targetValue - startValue;

            if (time <= tweenDuration)
            {
                time += Time.deltaTime;
                return exampleValue = (int)TweenManager.LinearTween(time, startValue, change, tweenDuration);
            }
            return exampleValue;
        }

        IEnumerator MoveShip()
        {
            while (true)
            {
                var movement = moveDirection * movementSpeed * Time.deltaTime;

                Vector3 currentPos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 targetPos = Camera.main.WorldToScreenPoint(transform.position + movement);

                if (currentPos.x < playSpace.leftX) currentPos.x++;
                if (currentPos.x > playSpace.rightX) currentPos.x--;
                if (currentPos.y < playSpace.leftY) currentPos.y++;
                if (currentPos.y > playSpace.rightY) currentPos.y--;

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