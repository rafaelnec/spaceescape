using UnityEngine;
using UnityEngine.EventSystems;
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
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private int previousMoveState;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        lookAction  = InputSystem.actions.FindAction("Look");

        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return; // Don't move the character!
        }
        ReadInputActions();
        MovePlayer();
        // AnimatePlayer();
    }

    void FixedUpdate()
    {
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
        
        if (transform.position != previousPosition || transform.rotation != previousRotation)
        {
            float moveX = 0;
            float moveY = 0;

            if (moveAction.IsPressed())
            {
                if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                {
                    if (moveInput.x > 0) moveX = 0.5f;
                    else moveX = -0.5f;

                    if (isRunning) moveX  *= 2;
                }
                else if (isRunning) moveY  = 1f;
                else moveY = 0.5f;
            } 
            else if (lookAction.WasPerformedThisFrame()) moveX = 0.25f;    

            playerAnimator.SetFloat("MoveX", moveInput.y > 0 ? moveX : -moveX);
            playerAnimator.SetFloat("MoveY", moveInput.y > 0 ? moveY : -moveY);
            playerAnimator.SetFloat("MoveSpeed", playerAnimatioMoveSpeed);

            previousPosition = transform.position;
            previousRotation = transform.rotation;
        } else
        {
            playerAnimator.SetFloat("MoveX", 0);
            playerAnimator.SetFloat("MoveY", 0);
        }
    }
    
}
