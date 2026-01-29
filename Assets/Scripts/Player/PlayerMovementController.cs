using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private CharacterController controller;


    private Vector2 moveInput;
    private Vector2 lookInput;

    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    
    public Transform playerCamera;

    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); 
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>(); 
    }

    void Update()
    {

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        // transform.position += moveDirection * moveSpeed * Time.deltaTime;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // velocity.y += gravity * Time.deltaTime;
        // controller.Move(velocity * Time.deltaTime);

        float mouseX = lookInput.x * lookSpeed * Time.deltaTime;
        float mouseY = lookInput.y * lookSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
