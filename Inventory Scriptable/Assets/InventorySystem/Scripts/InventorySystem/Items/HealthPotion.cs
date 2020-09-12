using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "InventorySystem/Items/HealthPotion")]
public class HealthPotion : Item, IUseable
{
    [SerializeField] private int healAmount = 10;

    public bool Use()
    {
        Debug.Log("heal" + healAmount);

        return true;
    }
}
