using System.Collections;
using UnityEngine;

public class ProjectileBehavior : ProjectileData
{
    public IEnumerator Shoot(Transform target)
    {
        gameObject.SetActive(true);

        transform.position = playerTransform.position;
        transform.LookAt(target);

        while (lifetime >= 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            lifetime -= Time.deltaTime;
            yield return null;
        }

        SetInactive();
    }

    public void SetInactive()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        gameObject.SetActive(false);
    }
}