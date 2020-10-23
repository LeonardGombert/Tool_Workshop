using UnityEngine;

namespace Gameplay.Player
{
    public class MovementData : MonoBehaviour
    {
        [HideInInspector] public Vector3 moveDirection;
        [HideInInspector] public Vector3 screenSpacePosition;
        [HideInInspector] public float movementSpeed = 22f;
        [HideInInspector] public float vignetteSetting;
        public bool rescaled;

        // playspace change transition values
        protected float time;
        protected float changeLX, changeLY, changeRX, changeRY;
        [HideInInspector] public float tweenDuration = 3f;

        [HideInInspector] public float transitionSpeed = 2;

        [HideInInspector] public Vector4Bounds playspace = new Vector4Bounds();
    }
}