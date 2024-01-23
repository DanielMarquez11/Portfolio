using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet")] private GameObject player;
    private Rigidbody rigid;
    public float force;

    private DecreaseOpacity inkt;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        inkt = FindObjectOfType<DecreaseOpacity>();

        Vector3 direction = player.transform.position - transform.position;
        rigid.velocity = new Vector3(direction.x, direction.y, 0).normalized * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inkt.InktActivated();
            Destroy(gameObject);
        }
    }
}