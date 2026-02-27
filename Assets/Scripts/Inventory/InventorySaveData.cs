using System;
using System.Collections.Generic;

[Serializable]
public class InventorySaveData
{
    // We store IDs because we can't "save" a Sprite or GameObject directly
    public List<string> itemIds = new List<string>();
    
}