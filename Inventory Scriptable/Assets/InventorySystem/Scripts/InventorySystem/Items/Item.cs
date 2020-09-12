using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable
{
    [SerializeField] private int ID = 0;
    [SerializeField] private Sprite icon = null;
    [SerializeField] private int stackSize = 0;
    [SerializeField] [TextArea(2, 5)] private string description = "";

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