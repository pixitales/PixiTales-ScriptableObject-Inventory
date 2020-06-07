using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { ManaPotionL, HealthPotionL }

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("References")]
    [SerializeField] private Inventory inventory;
    //[SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private HandScript handScript;
    //[SerializeField] private BoolEvent onPauseEvent;

    [Header("Debugging Tools")]
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

    public Inventory MyInventory
    {
        get { return inventory; }
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

    //public void OnEnable() => onPauseEvent?.RegisterListener(OnPause);

    //private void OnDisable() => onPauseEvent?.UnregisterListener(OnPause);

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

        if (inventory.MySlot.Length > 0 && getSlots != null)
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

    public void AddItemType(ItemType itemType) // For Debugging
    {
        switch (itemType)
        {
            case ItemType.ManaPotionL:
                AddItem(inventory.MyItemDatabase.MyItems[0], 1);
                break;
            case ItemType.HealthPotionL:
                AddItem(inventory.MyItemDatabase.MyItems[1], 1);
                break;
        }
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
        HandScript.MyInstance.Drop();

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

    public void OnResume()
    {
        if (handScript.MyMoveable != null)
        {
            SlotManager fromSlot = FromSlot;

            handScript.Drop();
            fromSlot.MyIcon.color = Color.white;
        }
    }

    private void OnApplicationQuit()
    {
        inventory.MySlot = new Slot[inventory.MySlotAmount];
    }
}
