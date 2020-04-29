using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public InventoryData MyInventoryData { get; set; }

    public SaveData()
    {
        MyInventoryData = new InventoryData();
    }
}

[Serializable]
public class InventoryData
{
    public List<ItemData> MyItems { get; set; }

    public InventoryData()
    {
        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class ItemData
{
    public int MyID { get; set; }
    public int MyStackCount { get; set; }
    public int MySlotIndex { get; set; }

    public ItemData(int ID, int stackCount, int slotIndex)
    {
        MyID = ID;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
    }
}
