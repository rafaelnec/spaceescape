using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> currentItems = new List<InventoryItem>();
    public List<InventoryItem> allItemDatabase; // Drag all your items here
    
    private string SavePath => Path.Combine(Application.persistentDataPath, "satchel.dat");

    public void AddItem(InventoryItem item) {
        if (!currentItems.Contains(item)) {
            currentItems.Add(item);
            Debug.Log($"Picked up {item.itemName}.");
        }
    }

    public InventoryItem GetItem(InventoryItem item) {
        return currentItems.FirstOrDefault(i => i.id == item.id);
    }

    [ContextMenu("Save Inventory")]
    public void Save() {
        InventorySaveData data = new InventorySaveData();
        foreach (var item in currentItems) data.itemIds.Add(item.id);

        string json = JsonUtility.ToJson(data);
        string encrypted = EncryptHelper.Encrypt(json);
        File.WriteAllText(SavePath, encrypted);
        Debug.Log("Inventory encrypted and saved!");
    }

    [ContextMenu("Load Inventory")]
    public void Load() {
        if (!File.Exists(SavePath)) return;

        string encrypted = File.ReadAllText(SavePath);
        string json = EncryptHelper.Decrypt(encrypted);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        currentItems.Clear();
        foreach (var id in data.itemIds) {
            var item = allItemDatabase.FirstOrDefault(i => i.id == id);
            if (item != null) currentItems.Add(item);
        }
        Debug.Log("Inventory decrypted and loaded!");
    }
}