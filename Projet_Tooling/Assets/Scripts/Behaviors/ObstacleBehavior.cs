using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public int lives;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile")) lives--;
        if (lives <= 0) Destroy(gameObject);
    }
}
