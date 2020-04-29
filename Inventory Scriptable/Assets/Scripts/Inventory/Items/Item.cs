using UnityEngine;

public enum ItemType
{
    HealthPotion,
    ManaPotion,
    Helmet,
    Weapon,
    Chest,
    Boots,
}

public abstract class Item : ScriptableObject, IMoveable
{
    [SerializeField] private int ID;
    [SerializeField] private Sprite icon;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int stackSize;
    [SerializeField] [TextArea(2, 5)] private string description;

    public int MyID
    {
        get { return ID; }
        set { ID = value; }
    }

    public Sprite MyIcon
    {
        get { return icon; }
    }

    public int MyStackSize
    {
        get { return stackSize; }
    }

    private SlotManager slot;
    public SlotManager MySlot
    {
        get { return slot; }
        set { slot = value; }
    }

    public void RemoveItem()
    {
        MySlot.RemoveItem(1);
    }
}