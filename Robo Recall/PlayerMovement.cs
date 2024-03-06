using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int points;

    #region Movement

    [SerializeField] private float speed = 10f;
    private Rigidbody rb;
    private Vector2 moveInput;
    public bool canMove;
    public Animator Camera;

    #endregion

    #region Shooting

    public Camera camera;
    public Transform target;

    private KeyHolder keyHolder;

    #endregion

    #region Health

    private Health health;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        keyHolder = GetComponent<KeyHolder>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(ScreenShakeSprintPlayer());
            speed = 4;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StartCoroutine(ScreenShakeSprintPlayerCancel());
            speed = 2.5f;
        }
    }

    public IEnumerator ScreenShakeSprintPlayer()
    {
        Camera.SetBool("Sprint", true);
        yield return null;
    }
    public IEnumerator ScreenShakeSprintPlayerCancel()
    {
        Camera.SetBool("Sprint", false);
        Camera.SetBool("SprintCancel",false);
        yield return null;
    }

    private void OnEnable()
    {
        // Get the PlayerInput component to register for input actions
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions.Enable();
        playerInput.actions.FindAction("Move").performed += OnMove;
        playerInput.actions.FindAction("MovingController").performed += OnMovingController;
        playerInput.actions.FindAction("LookingController").performed += OnLookingController;
    }

    private void OnDisable()
    {
        // Get the PlayerInput component to register for input actions
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindAction("Move").performed -= OnMove;
        playerInput.actions.FindAction("MovingController").performed -= OnMovingController;
        playerInput.actions.FindAction("LookingController").performed -= OnLookingController;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnMovingController(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(moveInput);

        if (Physics.Raycast(ray, out hit))
        {
            target.transform.position = hit.point;
            transform.LookAt(target);
        }
    }

    public void OnLookingController(InputAction.CallbackContext context)
    {
        float speed = 0.20f;
        Vector3 lookDirection = new Vector3(Gamepad.current.rightStick.ReadValue().x, 0,
            Gamepad.current.rightStick.ReadValue().y);
        transform.rotation = Quaternion.LookRotation(lookDirection * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.velocity = movement.normalized * speed;
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}