using UnityEngine;

public class Car : MonoBehaviour
{
    public Wheel[] Wheels;
    public float Power;
    public float MaxAngle;

    private float _Forward;
    private float _Angle;
    [SerializeField] private float maxSpeed;

    public ColliderSettings ColliderSettings;
    public PlayerController PlayerController;

    public Rigidbody rb;
    [SerializeField] private bool Controller;
    [SerializeField] public bool CanMove;

    private void Start()
    {
        rb.centerOfMass = new Vector3(0, -1, 0);
    }

    private void Update()
    {
        if (!Controller)
        {
            if (CanMove)
            {
                // print(Power);
                PlayerController.enabled = false;
                _Forward = Input.GetAxis("Vertical");
                _Angle = Input.GetAxis("Horizontal");

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    // print("Ingedrukt");
                    ColliderSettings.ApplyDriftSettings();
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    // print("Losgelaten");
                    ColliderSettings.ApplyNormalSettings();
                }
            }
        }

        if (Controller)
        {
            PlayerController.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        foreach (Wheel _wheel in Wheels)
        {
            if (CanMove)
            {
                if (_Forward != 0)
                {
                    _wheel.Accelerate(_Forward * Power);
                    _wheel.m_WheelCollider.brakeTorque = 0;
                }
                else
                {
                    _wheel.Brake(10000);
                }

                _wheel.Turn(_Angle * MaxAngle);

                if (Input.GetKey(KeyCode.Space))
                {
                    _wheel.Brake(1000000);
                    _wheel.m_WheelCollider.motorTorque = 0;
                }

                // Limiteer de maximumsnelheid
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
            }
        }
    }
}