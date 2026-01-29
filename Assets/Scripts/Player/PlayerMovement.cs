using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    [SerializeField] private float _playerSpeed = 5.0f;
    [SerializeField] private float _jumpHeight = 2.0f;
    [SerializeField] private float _gravityValue = -9.81f; // Gravity must be applied manually

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Get input from keyboard (WASD/Arrow keys)
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // Move relative to the character's forward direction (optional: can also move relative to camera/world)
        move = transform.TransformDirection(move); 

        // Apply movement
        _controller.Move(move * Time.deltaTime * _playerSpeed);

        // Changes the height position of the character based on gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        // Note: For full 3D movement including jumping, more complex logic is needed.
        // The CharacterController.Move function requires absolute movement deltas.
    }
}