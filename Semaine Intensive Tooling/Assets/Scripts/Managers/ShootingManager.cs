using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class ShootingManager : MonoBehaviour
    {
        ProjectilePool projectilePool = null;
        public GameObject projectilePrefab;
        public int projectilePoolSize;
        private int fireCount;

        public Transform playerTransform;
        public Transform projectileContainer;
        public RectTransform playerReticule;

        private void Awake()
        {
            projectilePool = new ProjectilePool(projectilePrefab, projectilePoolSize, projectileContainer, playerTransform);
        }

        public void GE_PlayerShooting()
        {
            StartCoroutine(projectilePool.projectileList[fireCount++].Shoot(playerReticule));
        }        
    }
}