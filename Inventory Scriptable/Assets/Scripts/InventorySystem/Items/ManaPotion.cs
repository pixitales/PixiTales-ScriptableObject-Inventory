using UnityEngine;

[CreateAssetMenu(fileName = "ManaPotion", menuName = "InventorySystem/Items/ManaPotion")]
public class ManaPotion : Item, IUseable
{
    [SerializeField] private int healAmount;

    public bool Use()
    {
        Debug.Log("heal" + healAmount);

        return true;
    }
}
