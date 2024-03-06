using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform Player; // The object that the camera follows
    public Vector3 OffSet; // The offset of the camera from the player
    public Vector2 TouchPos;
    public Vector2 CurrentTouchPos;
    public Vector2 PrevTouchPos;
    public Vector2 DeltaPos;

    public float directionZ = 40f; // The vertical and depth offset of the camera from the player

    [SerializeField] private FixedJoystick _JoyStick;
    [SerializeField] private LayerMask _LayerMask;

    private Vector2 _mouseDeltaPosition, _prevMousePos, _currentMousePosition;

    private float _turnSensitivity = 70f; // The sensitivity of the camera movement

    private PlayerInput _playerInput; // The PlayerInput component that enables input detection
    private Movement _cameraMovement; // The movement of the camera
    private float _rotationSpeed = 2f; // The speed at which the camera rotates

    private float _maxDistance = 50;
    private Camera _camera;


    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _playerInput = GetComponent<PlayerInput>();
        _cameraMovement = new Movement();
        _cameraMovement.MouseMovement.Enable(); // Enable mouse movement
        _cameraMovement.IphoneMovements.Enable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide the mouse cursor
        OffSet = new Vector3(Player.position.x, Player.position.y,
            Player.position.z - directionZ); //Set the initial camera offset
    }

    private void Update()
    {
        TouchInput();
        RayCastForCameraView();
    }

    private void RayCastForCameraView()
    {
        Debug.DrawRay(Player.position, -transform.forward * directionZ, Color.green);

        // Cast a ray from the camera's position towards the player's position
        Ray ray = new Ray(Player.position, -transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _LayerMask))
        {
            // Calculate the new camera position based on the hit point and normal
            Vector3 newPosition = hit.point;
            transform.position = newPosition;

            Debug.DrawRay(Player.position, -transform.forward * directionZ, Color.red);
        }
    }

    private void MouseInput()
    {
        transform.position = Player.transform.position - OffSet; // Move the camera to the 
        _currentMousePosition =
            Mouse.current.position.ReadValue(); // Read the current mouse position

        _mouseDeltaPosition =
            _currentMousePosition - _prevMousePos; // Calculate the delta position of the mouse
        _prevMousePos = _currentMousePosition;

        float mouseX =
            Mouse.current.delta.ReadValue().x; // Read the mouse movement along the x-axis
        OffSet = Quaternion.AngleAxis(mouseX * _turnSensitivity, Vector3.up) *
                 OffSet; // Calculate the new offset of the camera
    }

    private void TouchInput()
    {
        OffSet = Quaternion.AngleAxis(_JoyStick.Horizontal * _turnSensitivity * Time.deltaTime,
            Vector3.up) * OffSet;
        Debug.Log(_JoyStick.Horizontal);
        Quaternion targetRotation = Quaternion.LookRotation(Player.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
            _rotationSpeed * Time.deltaTime);

        transform.position = Player.position + OffSet; // Set the new position of the camera
        transform.LookAt(Player.position); // Look at the player
    }
}
