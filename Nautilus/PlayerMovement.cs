using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float speed = 40f;
    public Vector2 movement;
    public Rigidbody rb;

    [Header("Shooting")] [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public GameObject bulletToFire;

    //Speed Power Up
    private float speedDuration;
    private float speedBoost = 70;

    //Kwal Stun
    [Header("Stun")] public float stunDuration = 0.0f;
    public bool stun = false;

    // CoolDown Shooting
    [Header("Shooting CoolDown")] public float coolDown;
    float lastShot;

    [Header("ParticleSystem")] public Transform ParticleSystemLightning;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ParticleSystemLightning.GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCharacter(movement);
        movement = movement.normalized;
    }

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")); //* this is the controls of the player 


        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKey(KeyCode.Space)) // these are the keys to shoot a Bullet
        {
            ShootBullet(); // this actually shoots the bullet
        }

        //SpeedPowerUp
        if (speedDuration > 0)
        {
            speedDuration -= Time.deltaTime;
            speed = speedBoost;
        }
        else
        {
            speed = 40;
        }

        //Kwal stun
        if (stun == true)
        {
            ParticleSystemLightning.GetComponent<ParticleSystem>().enableEmission = true;

            stunDuration += Time.deltaTime;
            speed = 2;
        }

        if (stunDuration >= 3)
            stun = false;

        if (stun == false && speedDuration == 0)
        {
            stunDuration = 0;
            speed = 40;

            ParticleSystemLightning.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }

    /// <summary>
    /// this function moves the submarine
    /// </summary>
    public void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    public void ActivateSpeed()
    {
        speedDuration = 4f;
        Debug.Log("kaas");
    }

    //Kwal Stun
    public void PlayerStunned()
    {
        stun = true;
    }

    public void ShootBullet()
    {
        if (Time.time - lastShot < coolDown) return;

        lastShot = Time.time;
        var Bullet = Instantiate(bulletToFire, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

        Physics.IgnoreCollision(Bullet.GetComponent<Collider>(), GetComponent<Collider>());
    }
}