﻿using System;
using UnityEngine;

[Serializable]
public class Slot
{
    [SerializeField] private Item item = null;
    [SerializeField] private int ID = 0;
    [SerializeField] private int slotindex = 0;
    [SerializeField] private int stackCount = 0;

    public Item MyItem
    {
        get { return item; }
        set { item = value; }
    }

    public int MyItemID
    {
        get { return ID; }
        set { ID = value; }
    }

    public int MyStackCount
    {
        get { return stackCount; }
        set { stackCount = value; }
    }

    public bool IsEmpty
    {
        get
        {
            return stackCount == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || stackCount < MyItem.MyStackSize)
            {
                return false;
            }

            return true;
        }
    }

    public int MySlotIndex
    {
        get { return slotindex; }
        set { slotindex = value; }
    }

    public void AddItem(Item item)
    {
        this.item = item;
    }

    public void AddAmount(int amount)
    {
        this.stackCount += amount;
    }

    public void Clear()
    {
        item = null;
        stackCount = 0;
        ID = 0;
    }
}
