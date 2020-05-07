using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private HandScript handScript;
    [SerializeField] private bool debugMode;

    private Dictionary<SlotManager, Slot> slotDictionary;
    public Dictionary<SlotManager, Slot> MySlotDictionary
    {
        get { return slotDictionary; }
        private set { slotDictionary = value; }
    }

    // The slot we are moving an item with click
    private SlotManager fromSlot;
    public SlotManager FromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;

            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    public HandScript MyHandScript
    {
        get { return handScript; }
        set { handScript = value; }
    }

    public bool DebugMode
    {
        get { return debugMode; }
    }

    private SlotManager[] slotManager;
    public SlotManager[] SlotManager
    {
        get { return slotManager; }
        set { slotManager = value; }
    }

    private void Awake()
    {
        AddSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddItem(inventory.MyItemDatabase.MyItems[0], 1);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AddItem(inventory.MyItemDatabase.MyItems[1], 1);
        }
    }

    public void AddSlots()
    {
        MySlotDictionary = new Dictionary<SlotManager, Slot>();

        SlotManager[] getSlots = GetComponentsInChildren<SlotManager>();

        if (getSlots != null)
        {
            for (int i = 0; i < getSlots.Length; i++)
            {
                getSlots[i].Initialize(i, inventory.MySlot[i], this);
                slotDictionary.Add(getSlots[i], inventory.MySlot[i]);
            }
        }
        else if (debugMode)
        {
            Debug.Log("Cannot find slotPrefab in the scene or you did not put them as child of this parent");
        }

        inventory.AutoAssignSlotID();
    }

    public void AddItem(Item item, int amount)
    {
        foreach (KeyValuePair<SlotManager, Slot> slot in slotDictionary)
        {
            if (item.MyStackSize > 0 && !slot.Value.IsEmpty)
            {
                if (PlaceInStack(item, amount, slot.Value))
                {
                    slot.Key.UpdateSlot();

                    return;
                }
            }

            if (slot.Value.IsEmpty)
            {
                slot.Key.AddItem(item, amount);

                return;
            }
        }
    }

    private bool PlaceInStack(Item item, int amount, Slot slot)
    {
        if (item.MyID == slot.MyItem.MyID && slot.MyStackCount + amount <= item.MyStackSize)
        {
            slot.MyStackCount += amount;

            return true;
        }

        return false;
    }

    public void SortItems()
    {
        Slot[] getSlots = inventory.MySlot;

        inventory.MySlot = getSlots
            .OrderByDescending(o => o.MyItemID)
            .ThenByDescending(o => o.MyStackCount)
            .ToArray();

        UpdateInventory();
    }

    private void UpdateInventory()
    {
        // Reassign slot ID
        AddSlots();

        foreach (KeyValuePair<SlotManager, Slot> slot in slotDictionary)
        {
            slot.Key.UpdateSlot();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.MySlot = new Slot[25];
    }
}
