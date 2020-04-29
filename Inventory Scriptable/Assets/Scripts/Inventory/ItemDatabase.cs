using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "InventorySystem/Items/Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private Item[] items;

    public Item[] MyItems
    {
        get { return items; }
    }

    // Automatically assign item ID numbers
    public void OnValidate()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].MyID = i + 1;
            }
        }
    }
}
