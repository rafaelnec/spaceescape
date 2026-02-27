using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PadTrigger : MonoBehaviour
{
    public UnityEvent onPadClicked;

    public GameObject displayLock;
    public GameObject displayUnlock;

    public string requiredItemId = "access_card_lvl_01";
    private bool hasRequiredItem = false;

    private bool isDoorLock = true;
    
    private InputAction interactAction;
    private InventoryManager inventoryManager;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        UpdateDoorDisplay();
    }

    void OnTriggerStay(Collider other)
    {
        if (interactAction.WasReleasedThisFrame() && hasRequiredItem)
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
        if (inventoryManager != null) {
            hasRequiredItem = inventoryManager.currentItems.Exists(item => item.id == requiredItemId);
        }
    }

}
