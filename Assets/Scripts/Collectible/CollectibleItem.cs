using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private InventoryItem data; 

    public InventoryItem GetInventoryItem()
    {
        return data;
    }
}
