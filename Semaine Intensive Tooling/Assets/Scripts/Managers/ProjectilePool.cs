using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : Pool
{
    public List<ProjectileBehavior> projectileList = new List<ProjectileBehavior>();

    public ProjectilePool(GameObject _prefab, int _count)
    {
        projectileList = Create<ProjectileBehavior>(_prefab, _count);
    }
}

public class Pool
{
    public List<T> Create<T>(GameObject _projectilePrefab, int _poolSize) where T : MonoBehaviour
    {
        List<T> newPool = new List<T>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject newObject = GameObject.Instantiate(_projectilePrefab, Vector3.zero, Quaternion.identity);

            T projectile = newObject.GetComponent<T>();

            newPool.Add(projectile);
        }

        return newPool;
    }
}
