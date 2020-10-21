using UnityEngine;

namespace Gameplay.Player
{
    public class MovementData : MonoBehaviour
    {
        [HideInInspector] public Vector3 moveDirection;
        [HideInInspector] public Vector3 screenSpacePosition;
        [HideInInspector] public float movementSpeed = 22f;

        [HideInInspector] public Vector4Bounds playSpace = new Vector4Bounds();
    }
}

[System.Serializable]
public struct Vector4Bounds
{
    public float leftX, leftY, rightX, rightY;
    public Vector4Bounds(float leftX, float leftY, float rightX, float rightY)
    {
        this.leftX = leftX;
        this.leftY = leftY;
        this.rightX = rightX;
        this.rightY = rightY;
    }
}