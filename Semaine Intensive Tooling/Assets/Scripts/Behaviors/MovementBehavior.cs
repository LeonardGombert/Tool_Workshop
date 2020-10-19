using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class MovementBehavior : MovementData
    {
        private void Awake()
        {
            camera = Camera.main;
            constraints1 = new Vector3(0 + 100, 0 + 100);
            constraints2 = new Vector3(camera.pixelWidth - 100, camera.pixelHeight - 100);
            StartCoroutine(MoveShip());
        }


       /* private void FixedUpdate()
        {
            var movement = direction * movementSpeed * Time.deltaTime;

            Vector3 targetPos = camera.WorldToScreenPoint(transform.position + movement);

            if (targetPos.x > constraints1.x && targetPos.y > constraints1.y &&
                targetPos.x < constraints2.x && targetPos.y < constraints2.y)
                transform.position += movement;
            
            else return;
        }*/

        IEnumerator MoveShip()
        {
            while(true)
            {
                var movement = direction * movementSpeed * Time.deltaTime;

                Vector3 targetPos = camera.WorldToScreenPoint(transform.position + movement);

                if (targetPos.x > constraints1.x && targetPos.y > constraints1.y &&
                    targetPos.x < constraints2.x && targetPos.y < constraints2.y)
                    transform.position += movement;

                yield return null;
            }
        }
    }
}