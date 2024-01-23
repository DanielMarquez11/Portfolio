using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnitFour : MonoBehaviour
{
    private ScreenShake screenShake;
    private PlayerMovement playerMov;
    private PlayerLife playerLife;
    private PlayerManager playerManager;

    [SerializeField] private int enemyUnitFourHealth = 60;

    [SerializeField] private Slider healtBar;
    public Transform targetFollow;
    public Vector3 offset;

    [SerializeField] private GameObject enemyUnitFour;

    [Header("ParticleSystem")] public Transform ParticleSystemLightning;

    private void Start()
    {
        screenShake = FindObjectOfType<ScreenShake>();
        playerMov = FindObjectOfType<PlayerMovement>();
        playerLife = FindObjectOfType<PlayerLife>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        //Healthbar
        healtBar.value = enemyUnitFourHealth;
        targetFollow.position = transform.position + offset;

        ParticleSystemLightning.GetComponent<ParticleSystem>().enableEmission = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMov.PlayerStunned();
            screenShake.StartCoroutine(screenShake.Shaking());
        }
    }

    public void DamageTakenUnitFour()
    {
        enemyUnitFourHealth = enemyUnitFourHealth - playerLife.playerDamage;

        if (enemyUnitFourHealth <= 0)
        {
            playerManager.playerExpAdded = 30;
            playerManager.ExperienceAdded();
            Destroy(enemyUnitFour);
        }
    }
}
