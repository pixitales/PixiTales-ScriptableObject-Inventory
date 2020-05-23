using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryGUI : Editor
{
    private Inventory inventory;
    private bool[] showItem;

    public override void OnInspectorGUI()
    {
        if (inventory == null)
        {
            inventory = (Inventory)target;
            showItem = new bool[inventory.MySlotAmount];
        }

        inventory.MyItemDatabase = (ItemDatabase)EditorGUILayout.ObjectField("Item Database: ", inventory.MyItemDatabase, typeof(Object), false);
        inventory.MySlotAmount = EditorGUILayout.IntField("Slot Amount", inventory.MySlotAmount);

        EditorGUIUtility.labelWidth = 80;
        EditorGUIUtility.fieldWidth = 60;

        for (int i = 0; i < inventory.MySlot.Length; i++)
        {
            if (inventory.MySlot[i].MyItem != null)
            {
                showItem[i] = EditorGUILayout.BeginFoldoutHeaderGroup(showItem[i], "Item slot " + i + " - " + inventory.MySlot[i].MyItem.name);

                if (showItem[i])
                {
                    EditorGUILayout.BeginVertical("box");

                    inventory.MySlot[i].MyItem = (Item)EditorGUILayout.ObjectField("Item", inventory.MySlot[i].MyItem, typeof(Object), false);

                    EditorGUILayout.BeginHorizontal();
                    inventory.MySlot[i].MyItemID =  EditorGUILayout.IntField("ID", inventory.MySlot[i].MyItem.MyID);
                    inventory.MySlot[i].MyStackCount = EditorGUILayout.IntField("StackCount", inventory.MySlot[i].MyStackCount);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            else
            {
                EditorGUILayout.LabelField("Item slot " + i + " is empty");
            }
        }

        if (GUI.changed)
            EditorUtility.SetDirty(inventory);
    }
}
