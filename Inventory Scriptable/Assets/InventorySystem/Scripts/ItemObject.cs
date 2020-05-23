using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pickup" + item.name);

            //InventoryManager.MyInstance.AddItem(item, amount);
            //Destroy(this.gameObject);
        }
    }
}
