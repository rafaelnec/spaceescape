using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private GameObject itemPrefab;

    private InventoryManager inventoryManager;
    private InputAction inventoryAction;
    private bool isInventoryOpen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryAction.WasReleasedThisFrame())
        {
            ToggleInventory();
        }
    }   
    
    public void ToggleInventory() {

        this.gameObject.GetComponent<PlayerMovementController>().enabled = isInventoryOpen;
        isInventoryOpen = !isInventoryOpen;
        inventoryScreen.SetActive(isInventoryOpen);

        if (isInventoryOpen) {
            LoadInventory();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; 
        } else {
            ClearInventory();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    private void LoadInventory()
    {
        if (inventoryManager != null) {
            List<InventoryItem> items = inventoryManager.currentItems;
            foreach (InventoryItem item in items) {
                GameObject newItem = Instantiate(itemPrefab, inventoryContent.transform, false);
                newItem.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
                newItem.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = item.itemName;
            }
        }
    }

    private void ClearInventory()
    {
        foreach (Transform child in inventoryContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
