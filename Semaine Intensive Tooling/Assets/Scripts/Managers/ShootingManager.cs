using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class ShootingManager : MonoBehaviour
    {
        ProjectilePool projectilePool = null;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] int projectilePoolSize;
        private int fireCount;

        [SerializeField] Transform playerTransform;
        [SerializeField] Transform projectileContainer;
        [SerializeField] RectTransform playerReticule;

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