using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollectible : MonoBehaviour
{
    private InputAction interactAction;
    private InventoryManager inventoryManager;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            Collect(other.gameObject);
        }

    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            Collect(other.gameObject);
        }

    }

    private void Collect(GameObject gameObject)
    {
        if (interactAction.WasReleasedThisFrame())
        {
            if (inventoryManager != null) {
                CollectibleItem collectibleItem = gameObject.GetComponent<CollectibleItem>();
                if (collectibleItem != null)
                {
                    InventoryItem inventoryIteminventoryItem = collectibleItem.GetInventoryItem();
                    if (inventoryIteminventoryItem != null) {
                        inventoryManager.AddItem(inventoryIteminventoryItem);
                        Destroy(gameObject);
                    }
                }
                
            }
        }
    }
}
