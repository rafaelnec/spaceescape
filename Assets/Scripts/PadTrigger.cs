using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PadTrigger : MonoBehaviour
{
    public UnityEvent onPadClicked;

    public GameObject displayLock;
    public GameObject displayUnlock;

    private bool isDoorLock = true;
    
    private InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void OnTriggerEnter(Collider other)
    {
        UpdateDoorDisplay();
    }

    void OnTriggerStay(Collider other)
    {
        if (interactAction.WasReleasedThisFrame())
        {
            onPadClicked.Invoke();
            isDoorLock = false;
            UpdateDoorDisplay();
        }
    }

    void OnTriggerExit(Collider other)
    {
        displayLock.SetActive(false);
        displayUnlock.SetActive(false);
    }

    private void UpdateDoorDisplay()
    {
        displayLock.SetActive(isDoorLock);
        displayUnlock.SetActive(!isDoorLock);
    }

}
