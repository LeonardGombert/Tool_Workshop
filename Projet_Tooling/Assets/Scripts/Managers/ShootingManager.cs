using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class ShootingManager : MonoBehaviour
    {
        [HideInInspector] public ProjectilePool projectilePool = null;
        [HideInInspector] public GameObject projectilePrefab;
        [HideInInspector] public int projectilePoolSize;
        private int fireCount;

        [HideInInspector] public Transform playerTransform;
        [HideInInspector] public Transform projectileContainer;
        [HideInInspector] public RectTransform playerReticule;

        public void GE_PlayerShooting()
        {
            StartCoroutine(projectilePool.projectileList[fireCount++].Shoot(playerReticule));
        }
    }
}