using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _BulletPrefab;
    [SerializeField] private float speed;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Transform EnemyTarget;
    [SerializeField] private Transform firePoint;

    public int currentClip, maxClipSize = 12, currentAmmo, maxAmmo = 100;

    // Reloading
    [SerializeField] private float ReloadCoolDownTimer;
    [SerializeField] private float ReloadCoolDownDuration;

    private PauseMenu _pauseMenu;

    [SerializeField] private Transform lookDirection;

    [SerializeField] private LayerMask _layerMask;

    private float TimeCount = 0f;

    public MeleeAttack _meleeAttack;

    private Start_melee_tutorial _meleeTutorial;


    [SerializeField] private Transform Gun;
    public Animator animator;
    public bool GunIsActive;

    // Sounds
    public AudioSource audioSource;
    public AudioClip AudioClip;
    public AudioClip AudioClipReloading;

    public Animator Camera;

    private bool isReloading;
    private float reloadTimer;

    private bool hasReloaded;

    private void Start()
    {
        _pauseMenu = GetComponent<PauseMenu>();

        // Create input actions
        InputActionMap actionMap = new InputActionMap();
        actionMap.AddAction("Shoot", InputActionType.Button, "<Mouse>/leftButton");
        actionMap.AddAction("Reload", InputActionType.Button, "<Keyboard>/r");
        actionMap.AddAction("ControllerShoot", InputActionType.Button, "<Gamepad>/buttonSouth");
        actionMap.AddAction("ReloadCheat", InputActionType.Button, "<Keyboard>/f5");
        actionMap.AddAction("Look", InputActionType.PassThrough, "<Mouse>/position");

        // Bind callbacks to input actions
        actionMap.FindAction("Shoot").performed += OnShoot;
        actionMap.FindAction("ControllerShoot").performed += OnControllerShoot;
        actionMap.FindAction("ReloadCheat").performed += OnReloadCheat;
        actionMap.FindAction("Look").performed += OnLook;

        _meleeTutorial = FindObjectOfType<Start_melee_tutorial>();
    }

    private void Update()
    {
        if (_meleeAttack.isLookingEnabled)
        {
            lookDirection.transform.position = transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, TimeCount * 0.03f);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            TimeCount = TimeCount + Time.deltaTime;
        }

        if (GunIsActive)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloading || !isReloading && currentClip <= 0)
            {
                if (currentAmmo > 0)
                {
                    if (!hasReloaded)
                    {
                        audioSource.PlayOneShot(AudioClipReloading);
                        hasReloaded = true;
                    }
                    StartReload();
                }
            }

            if (isReloading)
            {
                reloadTimer += Time.deltaTime;

                if (reloadTimer >= ReloadCoolDownDuration)
                {
                  
                    CompleteReload();
                }
            }
        }
    }

    private void OnEnable()
    {
        // Enable input actions
        GetComponent<PlayerInput>().actions.Enable();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (GunIsActive)
        {
            if (currentClip > 0)
            {
                StartCoroutine(ScreenShakeHitEnemy(0.25f));
                audioSource.PlayOneShot(AudioClip);
                Fire();
            }
        }
    }

    public IEnumerator ScreenShakeHitEnemy(float timer)
    {
        Camera.SetBool("PlayerHit", true);
        yield return new WaitForSeconds(timer);
        Camera.SetBool("PlayerHit", false);
    }

    #region Controller

    public void OnControllerShoot(InputAction.CallbackContext context)
    {
        if (GunIsActive)
        {
            if (context.performed)
            {
                Fire();
            }
        }
        else
        {
            return;
        }
    }

    #endregion

    private void Fire()
    {
        if (currentClip > 0)
        {
            GameObject bulletclone = Instantiate(_BulletPrefab, firePoint.position, transform.rotation);
            bulletclone.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            currentClip--;
        }
    }

    public void StartReload()
    {
        if (currentClip < maxClipSize && currentAmmo > 0 || currentClip <= 0)
        {
            isReloading = true;
            reloadTimer = 0f;
        }
    }

    public void CompleteReload()
    {
        int reloadAmount = maxClipSize - currentClip;
        reloadAmount = Mathf.Min(reloadAmount, currentAmmo);
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;

        isReloading = false;
        reloadTimer = 0f;
        hasReloaded = false;
    }

    public void OnReloadCheat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            maxAmmo = 100;
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(context.ReadValue<Vector2>());
        if (!_meleeTutorial.animationStarted)
        {
            if (_meleeAttack.isLookingEnabled)
            {
                if (Physics.Raycast(ray, out hit, _layerMask))
                {
                    target.transform.position = hit.point;
                    transform.LookAt(target);
                }
            }
        }
        else if (_meleeTutorial.animationStarted)
        {
            _meleeAttack.isLookingEnabled = true;
            if (_meleeAttack.isLookingEnabled)
            {
                if (Physics.Raycast(ray, out hit, _layerMask))
                {
                    target.transform.position = hit.point;
                    transform.LookAt(EnemyTarget);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            Gun.gameObject.SetActive(true);
            GunIsActive = true;
            Destroy(other.gameObject);
           
        }
    }
}