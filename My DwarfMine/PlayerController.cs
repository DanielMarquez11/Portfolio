using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Wheel[] Wheels;
    public bool BrakingIsPressed = false;
    public float Power;
    public float MaxAngle;
    public float DownForceMagnitude;

    private float _moveSpeed = 10f;
    private float _Forward;
    private float _Angle;

    private PlayerControls _controls = null;
    private Vector2 _SteeringVector = Vector2.zero;
    private Rigidbody _rb = null;

    private void Awake()
    {
        _controls = new PlayerControls();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Driving.MoveWheels.performed += OnSteeringPerformed;
        _controls.Driving.MoveWheels.canceled += OnSteeringCancelled;
        _controls.Driving.Drive.performed += OnMovingPerformed;
        _controls.Driving.Drive.canceled += OnMovingCancelled;
        _controls.Driving.Brake.performed += OnBrakingPerformed;
        _controls.Driving.Brake.canceled += OnBrakingCancelled;
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Driving.MoveWheels.performed -= OnSteeringPerformed;
        _controls.Driving.MoveWheels.canceled -= OnSteeringCancelled;
        _controls.Driving.Drive.performed -= OnMovingPerformed;
        _controls.Driving.Drive.canceled -= OnMovingCancelled;
        _controls.Driving.Brake.performed -= OnBrakingPerformed;
        _controls.Driving.Brake.canceled -= OnBrakingCancelled;
    }

    private void FixedUpdate()
    {
        // Apply additional forces for better road grip (e.g., downforce).
        Vector3 downForce = -transform.up * DownForceMagnitude;
        _rb.AddForce(downForce, ForceMode.Force);

        foreach (Wheel _wheel in Wheels)
        {
            _wheel.Accelerate(_Forward * Power);
            _wheel.Turn(_Angle * MaxAngle);

            if (Input.GetKey(KeyCode.Space) && BrakingIsPressed)
            {
                _wheel.Brake(Power * 100);
            }
            else if(!BrakingIsPressed)
            {
                _wheel.m_WheelCollider.brakeTorque = 0;
            }
        }
    }

    private void OnSteeringPerformed(InputAction.CallbackContext value)
    {
        // print(value.ReadValue<Vector2>().x.ToString());
        _SteeringVector = value.ReadValue<Vector2>().normalized;
        _Angle = _SteeringVector.x;
    }

    private void OnMovingPerformed(InputAction.CallbackContext value)
    {
        print("performed");
        _Forward = 1;
    }

    private void OnMovingCancelled(InputAction.CallbackContext value)
    {
        print("Cancelled");
        _Forward = 0;
        Power = 0;
    }

    private void OnBrakingPerformed(InputAction.CallbackContext value)
    {
        BrakingIsPressed = true;

        print(BrakingIsPressed.ToString());
    }

    private void OnBrakingCancelled(InputAction.CallbackContext value)
    {
        BrakingIsPressed = false;
        
        print("Cancelled");
    }

    private void OnSteeringCancelled(InputAction.CallbackContext value)
    {
        _SteeringVector = Vector2.zero;
        _Angle = _SteeringVector.x;
    }
}