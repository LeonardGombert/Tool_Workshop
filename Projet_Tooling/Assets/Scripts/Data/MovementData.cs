using UnityEngine;

namespace Gameplay.Player
{
    public class MovementData : MonoBehaviour
    {
        [HideInInspector] public Vector3 moveDirection;
        [HideInInspector] public Vector3 screenSpacePosition;
        [HideInInspector] public float movementSpeed = 22f;

        // playspace change transition values
        protected float time;
        protected float change;
        protected float startValue;
        protected float targetValue;
        [HideInInspector] public float tweenDuration;

        [HideInInspector] public Vector4Bounds playSpace = new Vector4Bounds();
    }
}