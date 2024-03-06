using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private LayerMask _JumpableGround;
	[SerializeField] private FixedJoystick _JoyStick;
	private float _Speed = 50f;
	private bool _isGrounded;
	
	private float _timer;
	private Vector3 _movementVector;
	private Rigidbody _player;

	private PlayerInput _playerInput;
	private Movement _playerMovement;
	private CameraController _controller;

	private Vector2 _inputVector;
	private Vector3 _moveDirection;
	private RaycastHit _hit;

	private Camera _cameraMain;
	private Vector2 _screenPosition;
	private Vector2 _directionInput;

	private CameraController _camera;

	private void Awake()
	{
		_cameraMain = Camera.main;
		_player = GetComponent<Rigidbody>();
		_playerInput = GetComponent<PlayerInput>();

		_playerMovement = new Movement();
	}

	private void Update()
	{
		Drag();
		if (GroundCheck())
		{
			_isGrounded = true;
		}
		
		else
		{
			_isGrounded = false;
		}
	}

	private void FixedUpdate()
	{
		float movespeed = 0.3f;
		Vector3 camForward = Camera.main.transform.forward;
		Vector3 camRight = Camera.main.transform.right;
		camForward.y = 0;
		camRight.y = 0;
		Vector3 movement = _JoyStick.Horizontal * camRight + _JoyStick.Vertical * camForward;
		movement = movement.normalized * movespeed;

		_player.AddForce(movement, ForceMode.VelocityChange);
	}

	private void OnEnable()
	{
		_playerMovement.Movementmap.Enable();
		_playerMovement.PlayerMovement.Enable();
		_playerMovement.IphoneMovements.Enable();
		_playerMovement.IphoneMovements.Gyro.Enable();
	}

	private void OnDisable()
	{
		_playerMovement.Movementmap.Disable();
		_playerMovement.PlayerMovement.Disable();
		_playerMovement.IphoneMovements.Disable();
	}

	private bool GroundCheck()
	{
		return Physics.Raycast(transform.position, Vector3.down, out _hit, 6f, _JumpableGround);
	}

	private void Drag()
	{
		if (_inputVector.x == 0 && _inputVector.y == 0)
		{
			_timer += Time.deltaTime;
			if (_timer > 0.2f)
			{
				_player.drag += 0.05f;
				_timer = 0;
			}
		}

		if (_player.drag > 1)
		{
			_player.drag = 1;
		}

		if (_inputVector.x > 0 || _inputVector.x < 0 || _inputVector.y > 0 || _inputVector.y < 0)
		{
			_player.drag = 0;
		}
	}

	private void Movement_Performed(InputAction.CallbackContext context)
	{
		_inputVector = context.ReadValue<Vector2>();
		float speed = 30f;
		_player.AddForce(_moveDirection * speed * Time.deltaTime, ForceMode.Force);
	}

	private void DirectionPerformed(InputAction.CallbackContext context)
	{
		_directionInput = context.ReadValue<Vector2>();
	}

	public void Move(InputAction.CallbackContext context)
	{
		float movespeed = 0.001f;
		Vector3 camForward = Camera.main.transform.forward;
		Vector3 camRight = Camera.main.transform.right;
		camForward.y = 0;
		camRight.y = 0;
		Vector3 movement = _JoyStick.Horizontal * camRight + _JoyStick.Vertical * camForward;
		movement = movement.normalized * movespeed * Time.fixedDeltaTime;
		_player.AddForce(movement, ForceMode.VelocityChange);
	}
	public void JumpTest()
	{
		if (_isGrounded)
		{
			Debug.Log("Jumped");
			_player.AddForce(Vector3.up * _Speed, ForceMode.Impulse);
		}
	}
}