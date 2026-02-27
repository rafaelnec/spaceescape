using UnityEngine;

public enum InventoryType
{
    Common,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string id;
    public string itemName;
    public float weight;
    public InventoryType type;
    public Sprite icon;
    public GameObject prefab; // Visual representation
}
