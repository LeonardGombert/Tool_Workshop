using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    ProjectilePool projectilePool;
    public GameObject projectilePrefab;
    public int poolSize;

    private void Awake()
    {
        projectilePool = new ProjectilePool(projectilePrefab, poolSize);
    }
}
