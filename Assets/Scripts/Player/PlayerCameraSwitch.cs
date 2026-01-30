using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraSwitch : MonoBehaviour
{

    [SerializeField] private CinemachineBrain brainCamera;
    [SerializeField] private CinemachineCamera firstPersonCamera;
    [SerializeField] private CinemachineCamera thirdPersonCamera;
    [SerializeField] private CinemachineCamera frontPersonCamera;

    private InputAction firstPersonCameraAction;
    private InputAction thirdPersonCameraAction;
    private InputAction frontPersonCameraAction;

    private CinemachineCamera primaryCamera;
    private CinemachineCamera previousPrimaryCamera;

    void Start()
    {
        firstPersonCameraAction = InputSystem.actions.FindAction("FirstPersonView");
        thirdPersonCameraAction = InputSystem.actions.FindAction("ThirdPersonView");
        frontPersonCameraAction = InputSystem.actions.FindAction("FrontPersonView");

        primaryCamera = (CinemachineCamera)brainCamera.ActiveVirtualCamera;
    }

    void Update()
    {
        if (firstPersonCameraAction.IsPressed())
            SetPrimaryCamera(firstPersonCamera);

        if (thirdPersonCameraAction.IsPressed())
             SetPrimaryCamera(thirdPersonCamera);
        
        if (frontPersonCameraAction.WasPerformedThisFrame())
             SetPrimaryCamera(frontPersonCamera);

        if (frontPersonCameraAction.WasCompletedThisFrame())
            SetPrimaryCamera(previousPrimaryCamera);
        
    }

    private void SetPrimaryCamera(CinemachineCamera newPrimaryCamera)
    {
        if (newPrimaryCamera != primaryCamera)
        {           
            previousPrimaryCamera = primaryCamera;
            previousPrimaryCamera.Priority = 1;
            primaryCamera = newPrimaryCamera;
            primaryCamera.Priority = 5;

            if(primaryCamera == firstPersonCamera) RemovePlayerHeadLayer();
            else Camera.main.cullingMask = -1;
        }
    }

    private void RemovePlayerHeadLayer()
    {
        Debug.Log("I am here");
        int LayerPlayerHead = LayerMask.NameToLayer("Player Head");
        if (LayerPlayerHead != -1) 
        {
            Debug.Log("I am here 2");
            Camera.main.cullingMask &= ~(1 << LayerPlayerHead);
        }
    }

}

