using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Mathematics;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float AttackRange;
    [SerializeField] private float ShakeRange;
    [SerializeField] private LayerMask attackLayermask;

    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.5f;
    public bool isDashing = false;
    [SerializeField] private float movementSpeed;

    public bool CoolDownDash = false;
    public float coolDownTimer = 8;

    public bool isLookingEnabled = true;

    public Camera camera;
    private PlayerMovement _playerMovement;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject textToDisable;
    [SerializeField] private Image DashFill;

    private float _yChargeStartHeight;

    [SerializeField] private GameObject[] PS_MeleeHit;
    [SerializeField] private Transform PS_Melee;

    // sounds
    [SerializeField] private AudioSource meleeAudioSource;

    [Tooltip("Plaats hier de sound van de melee")] [SerializeField]
    private AudioClip meleeAudioClip;

    [Tooltip("Plaats hier de sound van de meleeHit")] [SerializeField]
    private AudioClip meleeAudioHitClip;


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        PS_Melee.GetComponent<ParticleSystem>().enableEmission = false;
    }

    private void Update()
    {
        int coolDownTimerInt = Mathf.RoundToInt(coolDownTimer);

        if (coolDownTimer == 8)
        {
            text.text = "V";
        }
        else if (coolDownTimer < 8)
        {
            text.text = coolDownTimerInt.ToString();
            DashFill.fillAmount = coolDownTimer * 0.125f;
        }

        if (CoolDownDash)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (!isDashing && coolDownTimer < 0)
        {
            coolDownTimer = 8;
            CoolDownDash = false;
        }

        if (Input.GetKeyDown(KeyCode.V) && !isDashing && isLookingEnabled && !CoolDownDash)
        {
            if (!meleeAudioSource.isPlaying)
            {
                meleeAudioSource.PlayOneShot(meleeAudioClip);
            }

            CoolDownDash = true;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            _yChargeStartHeight = transform.position.y; //set the starting Y pos

            if (Physics.Raycast(ray, out hit, attackLayermask))
            {
                Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(ray.origin, hit.point), Color.blue);
                // Een object is geraakt, voer hier de gewenste acties uit
                StartCoroutine(DashCoroutine(hit));
                if (Vector3.Distance(ray.origin, hit.point) <= 1)
                {
                    if (!meleeAudioSource.isPlaying)
                    {
                        meleeAudioSource.PlayOneShot(meleeAudioHitClip);
                    }
                }

                _playerMovement.canMove = true;
            }
        }


        if (isDashing) //if we are dashing keep the y at a fixed value
        {
            PS_Melee.GetComponent<ParticleSystem>().enableEmission = true;
            transform.position = new Vector3(transform.position.x, _yChargeStartHeight, transform.position.z);
        }
        else
        {
            PS_Melee.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isDashing)
        {
            EnemyHealth health = other.collider.gameObject.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.enemyHealth -= 12;
            }
        }
    }

    private IEnumerator DashCoroutine(RaycastHit hit)
    {
        isDashing = true;
        float dashTimer = 0f;

        // Sla de huidige bewegingssnelheid op
        float originalSpeed = movementSpeed;

        Vector3 direction = hit.point - transform.position;
        direction.Normalize();
        // Pas de dash-snelheid toe
        movementSpeed = dashSpeed;
        isLookingEnabled = false;
        while (dashTimer < dashDuration || Vector3.Distance(transform.position, hit.point) > 1)
        {
            Debug.Log("Entered Dash Function");
            // Verplaats het spelobject naar voren met de dash-snelheid
            transform.position = Vector3.MoveTowards(transform.position, hit.point, dashSpeed * Time.deltaTime);
            dashTimer += Time.deltaTime;
            yield return null;
        }

        // Herstel de normale bewegingssnelheid
        movementSpeed = originalSpeed;
        isDashing = false;
        isLookingEnabled = true;
        // Controleer of de afstand tussen de huidige positie en het doelpunt minder dan of gelijk aan 1 is
        if (Vector3.Distance(transform.position, hit.point) <= 1)
        {
            // Speel het geluidje af
            meleeAudioSource.PlayOneShot(meleeAudioHitClip);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}