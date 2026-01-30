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
    public float moveRunningSpeed = 0.5f;
    public float lookSpeed = 2f;
    public float gravity = -9.81f;


    [Header("Actions")]
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction lookAction;

    
    public Transform playerCamera;

    private float xRotation = 0f;

    [Header("Animation")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float playerAnimatioMoveSpeed = 2f;
    private bool isRunning = false;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        lookAction  = InputSystem.actions.FindAction("Look");
    }

    void Update()
    {
        ReadInputActions();
        MovePlayer();
        AnimatePlayer();
    }

    private void ReadInputActions()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        lookInput = lookAction.ReadValue<Vector2>();
       
        if (sprintAction.WasPerformedThisFrame()) isRunning  = true;
        if (sprintAction.WasCompletedThisFrame()) isRunning  = false;             
    } 

    private void MovePlayer()
    {
        isGrounded = controller.isGrounded;
        float speed = moveSpeed;
        float mouseSpeed =lookSpeed;

        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (isRunning)
        {
            mouseSpeed += moveRunningSpeed;  
            speed += moveRunningSpeed;
        } 

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        controller.Move(moveDirection * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float mouseX = lookInput.x * mouseSpeed * Time.deltaTime;
        float mouseY = lookInput.y * mouseSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void AnimatePlayer()
    {
        int moveState;
        if (isRunning) moveState  = 2;
        else if (moveAction.IsPressed()) moveState = 1;
        else if (lookAction.WasPerformedThisFrame()) moveState = 3;
        else  moveState = 0;

        playerAnimator.SetInteger("MoveState", moveInput.y > 0 ? moveState : -moveState);
        playerAnimator.SetFloat("MoveSpeed", playerAnimatioMoveSpeed);
    }
    
}
