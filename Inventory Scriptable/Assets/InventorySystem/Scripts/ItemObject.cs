using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item item = null;
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pickup: " + item.name + ", amount: " + amount);

            //InventoryManager.MyInstance.AddItem(item, amount);
            //Destroy(this.gameObject);
        }
    }
}
