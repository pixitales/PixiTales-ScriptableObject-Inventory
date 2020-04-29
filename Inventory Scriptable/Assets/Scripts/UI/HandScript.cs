using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HandScript : MonoBehaviour
{
    private static HandScript instance;
    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Image icon;
    [SerializeField] private Vector3 offset;

    public IMoveable MyMoveable { get; set; }

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    private void Update()
    {
        //Makes sure that the icon follows the hand
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
    }

    /// <summary>
    /// Take a moveable in the hand, so that we can move it around
    /// </summary>
    /// <param name="moveable">The moveable to pick up</param>
    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        inventoryManager.FromSlot = null;
    }

    /// <summary>
    /// Deletes an item from the inventory
    /// </summary>
    public void DeleteItem()
    {
        if (MyMoveable is Item)
        {
            Item item = (Item)MyMoveable;

            if (item.MySlot != null)
            {
                //item.MySlot.Clear();
            }
        }

        Drop();

        inventoryManager.FromSlot = null;
    }
}
