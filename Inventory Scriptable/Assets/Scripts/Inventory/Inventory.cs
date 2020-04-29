using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "InventorySystem/PlayerInventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Slot[] slot = new Slot[25];

    public ItemDatabase MyItemDatabase
    {
        get { return itemDatabase; }
    }

    public Slot[] MySlot
    {
        get { return slot; }
        set { slot = value; }
    }

    public void AutoAssignSlotID()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].MySlotIndex = i;
        }
    }

    public Item GetItemDatabase(int ID)
    {
        return itemDatabase.MyItems[ID - 1];
    }

    public void ClearSlots()
    {
        slot = new Slot[25];
    }
}