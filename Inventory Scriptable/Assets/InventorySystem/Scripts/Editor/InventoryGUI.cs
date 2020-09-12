using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryGUI : Editor
{
    private Inventory _inventory;
    private bool[] _showItem;

    public override void OnInspectorGUI()
    {
        if (_inventory == null)
        {
            _inventory = (Inventory)target;
            _showItem = new bool[_inventory.MySlotAmount];
        }

        _inventory.MyItemDatabase = (ItemDatabase)EditorGUILayout.ObjectField("Item Database: ", _inventory.MyItemDatabase, typeof(Object), false);
        _inventory.MySlotAmount = EditorGUILayout.IntField("Slot Amount", _inventory.MySlotAmount);

        EditorGUIUtility.labelWidth = 80;
        EditorGUIUtility.fieldWidth = 60;

        for (int i = 0; i < _inventory.MySlot.Length; i++)
        {
            if (_inventory.MySlot[i].MyItem != null)
            {
                _showItem[i] = EditorGUILayout.BeginFoldoutHeaderGroup(_showItem[i], "Item slot " + i + " - " + _inventory.MySlot[i].MyItem.name);

                if (_showItem[i])
                {
                    EditorGUILayout.BeginVertical("box");

                    _inventory.MySlot[i].MyItem = (Item)EditorGUILayout.ObjectField("Item", _inventory.MySlot[i].MyItem, typeof(Object), false);

                    EditorGUILayout.BeginHorizontal();
                    _inventory.MySlot[i].MyItemID =  EditorGUILayout.IntField("ID", _inventory.MySlot[i].MyItem.MyID);
                    _inventory.MySlot[i].MyStackCount = EditorGUILayout.IntField("StackCount", _inventory.MySlot[i].MyStackCount);
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
            EditorUtility.SetDirty(_inventory);
    }
}
