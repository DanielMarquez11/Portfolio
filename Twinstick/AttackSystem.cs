using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AttackSystem : MonoBehaviour
{
    private EnemyBehaviour _enemy;
    public NavMeshAgent _agent;

    [SerializeField] private Transform player;
    private float distanceToPlayer;
    private VisionCone _visionCone;

    // Shooting

    public GameObject EnemyBulletPrefab;
    public Transform EnemyDeployerBulletPrefab;
    public float speed = 10;
    public Transform firePoint;

    private bool weaponIsCoolingDown;
    public bool EnemyDeployer;
    
    public AudioSource audioSource;
    public AudioClip AudioClip;

    private void Start()
    {
        _enemy = GetComponent<EnemyBehaviour>();
        _agent = GetComponent<NavMeshAgent>();
        _visionCone = GetComponent<VisionCone>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    public void Attacking()
    {
        if (!EnemyDeployer)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= 2)
            {
                _agent.SetDestination(transform.position);
            }
            else
            {
                _agent.SetDestination(player.position);
            }
        }
        else
        {
            if (distanceToPlayer <= 2)
            {
                _agent.SetDestination(transform.position);
            }
            else
            {
                _agent.SetDestination(player.position);
            }
        }

        transform.LookAt(player.position);
    }


    public void Shooting()
    {
        if (!EnemyDeployer)
        {
            if (!weaponIsCoolingDown)
            {
                audioSource.PlayOneShot(AudioClip);
                weaponIsCoolingDown = true;
                GameObject bulletclone = Instantiate(EnemyBulletPrefab, firePoint.position, transform.rotation);
                StartCoroutine(WeaponCoolingDown(Random.Range(1, 3)));
                bulletclone.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            }
        }
        else if (EnemyDeployer)
        {
            if (!weaponIsCoolingDown)
            {
                audioSource.PlayOneShot(AudioClip);
                weaponIsCoolingDown = true;
                GameObject bulletclone = Instantiate(EnemyDeployerBulletPrefab.gameObject, firePoint.position, transform.rotation);
                StartCoroutine(WeaponCoolingDown(Random.Range(1, 3)));
                bulletclone.GetComponent<Rigidbody>().velocity = transform.forward * speed / 6;
            }
        }
    }

    private IEnumerator WeaponCoolingDown(float timer)
    {
        yield return new WaitForSeconds(timer);
        weaponIsCoolingDown = false;
    }

    public bool SeesPlayer()
    {
        return Vector3.Distance(transform.position, player.position) < 2;
    }
}