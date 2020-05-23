using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable
{
    [SerializeField] private int ID;
    [SerializeField] private Sprite icon;
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
}