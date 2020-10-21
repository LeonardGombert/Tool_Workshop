using UnityEngine;

namespace Gameplay.Player
{
    public class MovementData : MonoBehaviour
    {
        [HideInInspector] public Vector3 moveDirection;
        [HideInInspector] public Vector3 screenSpacePosition;
        [HideInInspector] public float movementSpeed = 22f;

        protected Camera camera;
        [HideInInspector] public Vector4Bounds playSpace = new Vector4Bounds();
    }
}

[System.Serializable]
public struct Vector4Bounds
{
    public float leftX, leftY, rightX, rightY;
}