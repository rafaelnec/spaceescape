using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private CharacterController controller;


    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private bool isGrounded;

    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float gravity = -9.81f;

    
    public Transform playerCamera;

    private float xRotation = 0f;
    private float yRotation = 0f;

    [Header("Animation")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float playerAnimatorWalkSpeed = 2f;
    private int walkingDirection = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); 

        if (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0)
        {
            walkingDirection =moveInput.y > 0 ? 1 : 2;
        }
        else
        {
            walkingDirection = 0;
        }
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>(); 
    }

    void Update()
    {

        isGrounded = controller.isGrounded;

        // Reset vertical velocity when grounded to prevent endless accumulation
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep them on the ground
        }

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        // transform.position += moveDirection * moveSpeed * Time.deltaTime;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float mouseX = lookInput.x * lookSpeed * Time.deltaTime;
        float mouseY = lookInput.y * lookSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log(walkingDirection);
        playerAnimator.SetInteger("WalkingDirection", walkingDirection); 
        playerAnimator.SetFloat("WalkingSpeed", playerAnimatorWalkSpeed); 
    }
}
