using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // when colliding the bullet destroys with a certain tags
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyBullet") || other.gameObject.CompareTag("Player") || other.gameObject.layer == 11 || other.gameObject.CompareTag("MainEnemy") || other.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
}