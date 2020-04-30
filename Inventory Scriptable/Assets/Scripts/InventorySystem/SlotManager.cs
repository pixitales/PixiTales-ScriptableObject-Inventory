using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void OnItemChanged(Item item);

public class SlotManager : MonoBehaviour, IPointerClickHandler
{
    public event OnItemChanged OnItemChangedEvent;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI stackCount;
    [SerializeField] private Image slotHighlight;

    private InventoryManager inventoryManager;

    public Image MyIcon
    {
        get { return icon; }
        private set { icon = value; }
    }

    public TextMeshProUGUI MyStackCount
    {
        get { return stackCount; }
        private set { stackCount = value; }
    }

    private Slot slot;
    public Slot MySlot
    {
        get { return slot; }
    }

    private int slotIndex;
    public int MySlotIndex
    {
        get { return slotIndex; }
    }

    public void Initialize(int slotIndex, Slot slot, InventoryManager inventoryManager)
    {
        this.slotIndex = slotIndex;
        this.slot = slot;
        this.inventoryManager = inventoryManager;

        //OnItemChangedEvent += new OnItemChanged(UpdateSlot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventoryManager.FromSlot == null && !slot.IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(slot.MyItem as IMoveable);

                inventoryManager.FromSlot = this;
            }
            else if (inventoryManager.FromSlot != null)
            {
                if (PutItemBack() || MergeItems(inventoryManager.FromSlot) || SwapItems(inventoryManager.FromSlot) || AddItem(inventoryManager.FromSlot))
                {
                    HandScript.MyInstance.Drop();

                    inventoryManager.FromSlot = null;
                }
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    private bool PutItemBack()
    {
        if (inventoryManager.FromSlot == this)
        {
            inventoryManager.FromSlot.MyIcon.color = Color.white;
            return true;
        }

        return false;
    }

    public void UseItem()
    {
        if (slot.MyItem is IUseable)
        {
            IUseable item = slot.MyItem as IUseable;

            if (item.Use())
            {
                RemoveItem(1);
            }
            else if (inventoryManager.DebugMode)
            {
                Debug.Log("Cannot use this " + item + " right now");
            }
        }
    }

    public bool AddItem(SlotManager from)
    {
        if (slot.IsEmpty || from.slot.MyItem.MyID == slot.MyItem.MyID)
        {
            if (inventoryManager.DebugMode)
                Debug.Log("Add Items");

            Item tmpItem = from.slot.MyItem;
            int count = from.slot.MyStackCount;

            for (int i = 0; i < count; i++)
            {
                if (slot.IsFull)
                {
                    return false;
                }

                AddItem(tmpItem, 1);
                from.ClearAllItems();
            }

            return true;
        }

        return false;
    }

    public void AddItem(Item item, int amount)
    {
        slot.AddItem(item);
        slot.AddAmount(amount);

        UpdateSlot();
    }

    public void RemoveItem(int amount)
    {
        if (!slot.IsEmpty)
        {
            slot.MyStackCount -= amount;
        }

        UpdateSlot();
    }

    public void ClearAllItems()
    {
        slot.MyStackCount = 0;
        UpdateSlot();
    }

    private bool MergeItems(SlotManager from)
    {
        if (slot.IsEmpty)
        {
            return false;
        }

        if (from.slot.MyItem.MyID == slot.MyItem.MyID && !slot.IsFull)
        {
            if (inventoryManager.DebugMode)
                Debug.Log("Merge Items");

            // How many free slots we have in the stack
            int free = slot.MyItem.MyStackSize - slot.MyStackCount;

            for (int i = 0; i < free; i++)
            {
                if (from.slot.MyStackCount > 0)
                {
                    int amount = 1;

                    AddItem(from.slot.MyItem, amount);
                    from.RemoveItem(amount);
                }
            }

            return true;
        }

        return false;
    }

    private bool SwapItems(SlotManager from)
    {
        if (slot.IsEmpty)
        {
            return false;
        }

        if (from.slot.MyItem.MyID != slot.MyItem.MyID || from.slot.MyStackCount + slot.MyStackCount > slot.MyItem.MyStackSize)
        {
            if (inventoryManager.DebugMode)
                Debug.Log("Swap Items");

            // Copy all the items we need to swap from A
            Item tmpItem = from.slot.MyItem;
            int tmpAmount = from.slot.MyStackCount;

            from.slot.MyItem = slot.MyItem;
            from.slot.MyStackCount = slot.MyStackCount;

            // All items from Slot B amnd copy them into Slot A
            slot.MyItem = tmpItem;
            slot.MyStackCount = tmpAmount;

            from.UpdateSlot();
            UpdateSlot();

            return true;
        }

        return false;
    }

    public void UpdateSlot()
    {
        if (slot.MyStackCount > 1)
        {
            icon.color = Color.white;
            icon.sprite = slot.MyItem.MyIcon;
            stackCount.text = slot.MyStackCount.ToString();
            slot.MyItemID = slot.MyItem.MyID;
        }
        else if (slot.MyStackCount == 1)
        {
            icon.color = Color.white;
            icon.sprite = slot.MyItem.MyIcon;
            stackCount.text = string.Empty;
            slot.MyItemID = slot.MyItem.MyID;
        }

        if (slot.IsEmpty)
        {
            icon.color = new Color(0, 0, 0, 0);
            stackCount.text = string.Empty;

            slot.MyItem = null;
            slot.MyStackCount = 0;
            slot.MyItemID = 0;
        }
    }

    public void OnItemChanged(Item item)
    {
        if (OnItemChangedEvent != null)
        {
            OnItemChangedEvent.Invoke(item);
        }
    }
}