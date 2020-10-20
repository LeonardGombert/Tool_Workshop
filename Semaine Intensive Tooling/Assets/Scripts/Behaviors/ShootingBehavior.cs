using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class ShootingBehavior : MonoBehaviour
    {
        // on input, call the Fire() method from a projectile in the pool
        private InputMapping inputMapping;
        private InputAction shootAction;

        public GameEvent shootingEvent;

        private void Awake()
        {
            inputMapping = new InputMapping();

            shootAction = inputMapping.Player.Fire;
            shootAction.performed += _ => shootingEvent.Raise();
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