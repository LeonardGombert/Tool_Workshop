using UnityEngine;

namespace Gameplay.Player
{
    public class MovementData : MonoBehaviour
    {
        public Vector3 moveDirection;
        protected float movementSpeed = 22f;

        protected Camera camera;
        protected Vector3 constraints1;
        protected Vector3 constraints2;
    }
}