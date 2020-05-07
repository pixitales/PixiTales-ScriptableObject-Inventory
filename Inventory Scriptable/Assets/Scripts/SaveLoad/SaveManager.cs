﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string savePath = "Inventory";

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Inventory playerInventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
    }

    public void Save()
    {
        Debug.Log("Saved");

        FileStream file = File.Open(Application.persistentDataPath + "/" + savePath + ".dat", FileMode.Create);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            SaveData data = new SaveData();

            SaveInventory(data);

            bf.Serialize(file, data);
        }

        catch (System.Exception)
        {
            Debug.Log("Failed to save");
        }

        finally
        {
            file.Close();
        }
    }

    public void Load()
    {
        Debug.Log("Loaded");

        FileStream file = File.Open(Application.persistentDataPath + "/" + savePath + ".dat", FileMode.Open);

        if (HasAnySaveFile())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                SaveData data = (SaveData)bf.Deserialize(file);

                LoadInventory(data);
            }

            catch (System.Exception)
            {
                Debug.Log("Failed to load");
            }

            finally
            {
                file.Close();
            }
        }
    }

    public void Delete()
    {
        File.Delete(Application.persistentDataPath + "/" + savePath + ".dat");
    }

    private void SaveInventory(SaveData data)
    {
        Slot[] getSlots = playerInventory.MySlot;

        for (int i = 0; i < getSlots.Length; i++)
        {
            data.MyInventoryData.MyItems.Add(new ItemData(getSlots[i].MyItemID, getSlots[i].MyStackCount, getSlots[i].MySlotIndex));
        }
    }

    private void LoadInventory(SaveData data)
    {
        SlotManager[] getSlots = inventoryManager.GetComponentsInChildren<SlotManager>();
        var getItems = data.MyInventoryData.MyItems;

        for (int i = 0; i < getItems.Count; i++)
        {
            int itemID = getItems[i].MyID;
            int stackCount = getItems[i].MyStackCount;
            int slotIndex = getItems[i].MySlotIndex;

            if (itemID >= 1)
            {
                playerInventory.MySlot[i].MyItem = playerInventory.GetItemDatabase(itemID);
                playerInventory.MySlot[i].MyItemID = itemID;
                playerInventory.MySlot[i].MyStackCount = stackCount;
            }

            getSlots[i].UpdateSlot();
        }
    }

    public bool HasAnySaveFile()
    {
        if (File.Exists(Application.persistentDataPath + "/" + savePath + ".dat"))
        {
            return true;
        }

        return false;
    }
}
