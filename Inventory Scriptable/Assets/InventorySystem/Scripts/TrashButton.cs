using UnityEngine;
using UnityEngine.EventSystems;

public class TrashButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryManager inventoryManager = null;
    //[SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private HandScript handScript = null;

    private Item _item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (handScript.MyMoveable is IUseable)
        {
            this._item = (Item)handScript.MyMoveable;

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
