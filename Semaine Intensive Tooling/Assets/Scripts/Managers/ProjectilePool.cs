using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Player;

public class ProjectilePool : Pool
{
    public List<ProjectileBehavior> projectileList = new List<ProjectileBehavior>();

    public ProjectilePool(GameObject _prefab, int _count, Transform _container, Transform _playerTransform)
    {
        projectileList = Create<ProjectileBehavior>(_prefab, _count, _container);

        foreach (ProjectileBehavior projectile in projectileList)
        {
            projectile.playerTransform = _playerTransform;
            projectile.SetInactive();
        }
    }
}

public class Pool
{
    public List<T> Create<T>(GameObject _projectilePrefab, int _poolSize, Transform _container) where T : MonoBehaviour
    {
        List<T> newPool = new List<T>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject newObject = GameObject.Instantiate(_projectilePrefab, Vector3.zero, Quaternion.identity, _container);

            T projectile = newObject.GetComponent<T>();

            newPool.Add(projectile);
        }

        return newPool;
    }
}
