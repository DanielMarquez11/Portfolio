using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyStateCheck
{
    Patrol,
    Attack
}

public enum EnemyDeployerStateCheck
{
    Patrol,
    Attack,
    Shoot,
    Follow,
    Explode
}

public class EnemyBehaviour : MonoBehaviour
{
    private PatrolSystem _patrolSystem;
    private AttackSystem _attackSystem;
    private VisionCone _visionCone;
    private Color currentColor;

    public EnemyStateCheck _enemyStateCheck;
    public EnemyDeployerStateCheck enemyDeployerStateCheck;
    private HomingBullet _homingBullet;
    [SerializeField] private Transform player;


    // Spraaklijnen
    public AudioSource Voices;
    public AudioClip[] voiceLines;
    public float voiceLineInterval = 10f;
    private float voiceLineTimer = 0f;

    private void Start()
    {
        _patrolSystem = GetComponent<PatrolSystem>();
        _attackSystem = GetComponent<AttackSystem>();
        _visionCone = GetComponent<VisionCone>();
        _homingBullet = FindObjectOfType<HomingBullet>();
        _enemyStateCheck = EnemyStateCheck.Patrol;
        enemyDeployerStateCheck = EnemyDeployerStateCheck.Patrol;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (gameObject.CompareTag("MainEnemy"))
        {
            switch (_enemyStateCheck)
            {
                case EnemyStateCheck.Patrol:
                    _patrolSystem.Patrol();
                    if (_attackSystem.SeesPlayer())
                    {
                        _enemyStateCheck = EnemyStateCheck.Attack;
                    }

                    break;
                case EnemyStateCheck.Attack:
                    // enemy attacks the player
                    if (!_attackSystem.SeesPlayer())
                    {
                        _enemyStateCheck = EnemyStateCheck.Patrol;
                    }

                    _attackSystem.Attacking();
                    _attackSystem.Shooting();
                    break;
            }

            // Timer voor spraaklijnen
            voiceLineTimer += Time.deltaTime;
            if (voiceLineTimer >= voiceLineInterval)
            {
                PlayRandomVoiceLine();
                voiceLineTimer = 0f;
            }
        }

        if (gameObject.CompareTag("EnemyDeployer"))
        {
            switch (enemyDeployerStateCheck)
            {
                case EnemyDeployerStateCheck.Patrol:
                    _patrolSystem.Patrol();
                    if (_attackSystem.SeesPlayer())
                    {
                        enemyDeployerStateCheck = EnemyDeployerStateCheck.Attack;
                    }

                    break;
                case EnemyDeployerStateCheck.Attack:
                    if (!_attackSystem.SeesPlayer())
                    {
                        enemyDeployerStateCheck = EnemyDeployerStateCheck.Patrol;
                    }

                    _attackSystem.Attacking();
                    _attackSystem.Shooting();

                    if (Vector3.Distance(_attackSystem.EnemyDeployerBulletPrefab.position, player.position) < 2)
                    {
                        enemyDeployerStateCheck = EnemyDeployerStateCheck.Explode;
                    }

                    break;
                case EnemyDeployerStateCheck.Explode:
                    Debug.Log("Exploding");

                    break;
            }
        }
    }

    private void PlayRandomVoiceLine()
    {
        if (voiceLines.Length > 0)
        {
            int randomIndex = Random.Range(0, voiceLines.Length);
            AudioClip voiceLine = voiceLines[randomIndex];
            Debug.Log(voiceLine);
            // Speel de voiceLine af, bijvoorbeeld via een AudioSource
            if (!Voices.isPlaying)
            {
                Voices.clip = voiceLines[randomIndex];
                Voices.Play();
            }
        }
    }
}