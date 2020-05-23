using UnityEngine;
using UnityEngine.EventSystems;

public class TrashButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryManager inventoryManager;
    //[SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private HandScript handScript;
    private Item item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (handScript.MyMoveable is IUseable)
        {
            this.item = (Item)handScript.MyMoveable;

            if (inventoryManager.FromSlot != null)
            {
                inventoryManager.FromSlot.ClearItems();
                HandScript.MyInstance.Drop();
            }
        }
    }

    public void DeleteItem()
    {
        inventoryManager.FromSlot.ClearItems();
        HandScript.MyInstance.Drop();
    }
}
