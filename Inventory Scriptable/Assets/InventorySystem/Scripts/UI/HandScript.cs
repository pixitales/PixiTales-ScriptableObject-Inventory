using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HandScript : Singleton<HandScript>
{
    [SerializeField] private InventoryManager inventoryManager = null;
    [SerializeField] private Vector3 offset = new Vector3(40, 10, 0);

    private Image _icon;
    public IMoveable MyMoveable { get; set; }

    private void Awake()
    {
        _icon = GetComponent<Image>();
        Initialize();
    }

    private void Initialize()
    {
        _icon.color = new Color(0, 0, 0, 0);
        _icon.raycastTarget = false;
    }

    private void Update()
    {
        //Makes sure that the icon follows the hand
        _icon.transform.position = Input.mousePosition + offset;

        // Deletes item if drag and drop item outside the menu
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyMoveable != null)
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
        _icon.sprite = moveable.MyIcon;
        _icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        _icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        _icon.color = new Color(0, 0, 0, 0);
        inventoryManager.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMoveable is Item)
        {
            inventoryManager.FromSlot.ClearItems();
        }

        Drop();

        inventoryManager.FromSlot = null;
    }
}
