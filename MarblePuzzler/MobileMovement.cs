using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Touch = UnityEngine.Touch;
using UnityEngine.InputSystem.EnhancedTouch;

public class MobileMovement : MonoBehaviour
{
	// Delegates and events for touch input
	public delegate void StartTouchEvent(Vector2 position, float time);

	public event StartTouchEvent OnStartTouch;

	public delegate void EndTouchEvent(Vector2 position, float time);

	public event EndTouchEvent OnEndTouch;
	[SerializeField] private float _Speed = 40f;

	// Player input and movement references
	private PlayerInput _playerInput;
	private Vector2 _inputVector;
	private Movement _playerMovement;
	private Vector3 _moveDirection;

	// Camera and screen position references
	private Camera _cameraMain;
	private Vector2 _screenPosition;

	// Timer for touch input duration
	private float _timer;

	// Awake method sets up references to player input and movement
	private void Awake()
	{
		_playerInput = GetComponent<PlayerInput>();
		_playerMovement = new Movement();
	}

	// Start method sets up touch input event handlers
	private void Start()
	{
		_playerMovement.IphoneMovements.Touchposition.started += StartTouch;
		_playerMovement.IphoneMovements.Touchposition.canceled += EndTouch;
	}

	// Update method calculates movement direction based on touch input and camera orientation
	private void Update()
	{
		Vector3 camForward = Camera.main.transform.forward;
		Vector3 camRight = Camera.main.transform.right;

		camForward.y = 0;
		camRight.y = 0;

		Vector3 forwardRelative = _inputVector.y * camForward;
		Vector3 rightRelative = _inputVector.x * camRight;

		_moveDirection = forwardRelative + rightRelative;
	}
	private void OnEnable()
	{
		_playerMovement.Movementmap.Enable();
		_playerMovement.PlayerMovement.Enable();
		_playerMovement.IphoneMovements.Enable();
		_playerMovement.MouseMovement.MouseMovement.Enable();
	}
	private void OnDisable()
	{
		_playerMovement.Movementmap.Disable();
		_playerMovement.PlayerMovement.Disable();
		_playerMovement.IphoneMovements.Disable();
		_playerMovement.MouseMovement.MouseMovement.Disable();
	}
	private void StartTouch(InputAction.CallbackContext context)
	{
		Debug.Log("Touch started" +
		          _playerMovement.IphoneMovements.Touchposition.ReadValue<Vector2>());
		if (OnStartTouch != null)
			OnStartTouch(_playerMovement.IphoneMovements.Touchposition.ReadValue<Vector2>(),
				(float)context.startTime);
	}
	private void EndTouch(InputAction.CallbackContext context)
	{
		Debug.Log("Touch Ended");
		if (OnEndTouch != null)
			OnEndTouch(_playerMovement.IphoneMovements.Touchposition.ReadValue<Vector2>(),
				(float)context.time);
	}
}
