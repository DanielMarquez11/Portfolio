using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielMarquez_VisionCone;
using Health;

public class StateManager : StateMachineBehaviour
{
    public Transform player;
    [SerializeField] public Vision _vision;
    [SerializeField] private Hiding _hiding;
    [SerializeField] public AudioClip _audioClip;
    [SerializeField] public AudioSource _audioSource;
    public float _distance;
    public bool isplayer = false;
    private float _timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _vision = animator.GetComponentInChildren<Vision>();
        _hiding = animator.GetComponent<Hiding>();
        _audioSource = animator.GetComponent<AudioSource>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        var Health = animator.GetComponent<HealthSystem>();
        _distance = Vector3.Distance(animator.transform.position, player.position);
        if (_vision != null && Health != null)
        {
            if (_distance <= 6 && _vision.playerInFOV || Health._currentHealth < Health._originalHealth)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.PlayOneShot(_audioClip);
                }

                animator.SetBool("Attack", true);
                Health._originalHealth = Health._currentHealth;
                animator.SetBool("IsPatrolling", false);
            }
            else if (_vision.fov >= 6)
            {
                _vision.playerInFOV = false;
                if (!_vision.playerInFOV || _hiding._isHiding)
                {
                    animator.SetBool("IsPatrolling", true);
                    animator.SetBool("Attack", false);
                }
            }
        }

        if (player != null && player.tag != "Player")
        {
            animator.SetBool("IsPatrolling", true);
            animator.SetBool("Attack", false);
        }

        if (player == null)
        {
            float MaxTimer = 10;
            _timer += Time.deltaTime;
            if (_timer >= MaxTimer)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        else
        {
            return;
        }
    }
}