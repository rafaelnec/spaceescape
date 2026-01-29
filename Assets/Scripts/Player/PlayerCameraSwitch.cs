using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraSwitch : MonoBehaviour
{

    [SerializeField] private CinemachineCamera firstPersonCamera;
    [SerializeField] private CinemachineCamera thirdPersonCamera;
    [SerializeField] private CinemachineCamera frontPersonCamera;

    private InputAction firstPersonCameraAction;
    private InputAction thirdPersonCameraAction;
    private InputAction frontPersonCameraAction;
    void Start()
    {
        firstPersonCameraAction = InputSystem.actions.FindAction("FirstPersonView");
        thirdPersonCameraAction = InputSystem.actions.FindAction("ThirdPersonView");
        frontPersonCameraAction = InputSystem.actions.FindAction("FrontPersonView");
    }

    void Update()
    {
        if (firstPersonCameraAction.IsPressed())
        {
            firstPersonCamera.Priority = 3;
            thirdPersonCamera.Priority = 2;
            frontPersonCamera.Priority = 1;
        }
        if (thirdPersonCameraAction.IsPressed())
        {
            thirdPersonCamera.Priority = 3;
            firstPersonCamera.Priority = 2;
            frontPersonCamera.Priority = 1;
        }
        if (frontPersonCameraAction.WasPerformedThisFrame())
        {
            frontPersonCamera.Priority = 3;
            firstPersonCamera.Priority = 2;
            thirdPersonCamera.Priority = 1;
        }
        if (frontPersonCameraAction.WasCompletedThisFrame())
        {
            thirdPersonCamera.Priority = 3;
            firstPersonCamera.Priority = 2;
            frontPersonCamera.Priority = 1;
        }
    }

}

