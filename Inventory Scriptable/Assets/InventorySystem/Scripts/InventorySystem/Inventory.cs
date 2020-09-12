using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "InventorySystem/PlayerInventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private ItemDatabase itemDatabase = null;
    [SerializeField] private int slotAmount = 25;
    [SerializeField] private Slot[] slot = new Slot[25];

    public ItemDatabase MyItemDatabase
    {
        get { return itemDatabase; }
        set { itemDatabase = value; }
    }

    public int MySlotAmount
    {
        get { return slotAmount; }
        set { slotAmount = value; }
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

    public bool HasItem(Item item)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (!slot[i].IsEmpty)
            {
                if (slot[i].MyItem == item)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int EmptySlotCount()
    {
        int count = 0;

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].IsEmpty)
                count++;
        }

        return count;
    }

    public void ClearAllSlots()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].Clear();
        }
    }
}