using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class MovementBehavior : MonoBehaviour
    {
        private InputAction movement;
        public CustomBindings customBindings;
        public float speed;
        Vector3 direction;

        private void Awake()
        {
            customBindings = new CustomBindings();

            movement = customBindings.Player.Move;
            movement.performed += ctx => Move(ctx);
            movement.canceled += ctx => Move(ctx);
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
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnDisable()
        {
            customBindings.Disable();
        }
    }
}
