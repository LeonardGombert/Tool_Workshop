using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private float lifetime;

    public IEnumerator Shoot()
    {
        transform.position += Vector3.forward;
        yield return new WaitForSeconds(lifetime);

        gameObject.SetActive(false);
    }
}
