using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    private InventoryItem _item;

    public InventoryItem Item
    {
        set
        {
            _item = value;

            gameObject.name = "Item Slot: " + _item.Data.DisplayName + " (" + _item.Quantity + ")";
            _icon.sprite = _item.Data.Icon;
            if (_item.Quantity > 1)
            {
                _amountText.SetText(_item.Quantity.ToString());
            }
            else
            {
                _amountText.SetText("");
            }
        }
    }

    private Image _icon;
    private TextMeshProUGUI _amountText;
    private RectTransform _rectTransform;

    /// <summary>
    /// Initialise the slot by getting the necessary components.
    /// </summary>
    public void Init()
    {
        gameObject.SetActive(true);

        _icon = GetComponentInChildren<Image>();
        _amountText = GetComponentInChildren<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Set the X and Y position of this slot in the UI of the Inventory
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ConsumableItemData consumableItemData = _item.Data as ConsumableItemData;
            if (consumableItemData != null)
            {
                consumableItemData.InvokeConsumeEvent();
                consumableItemData.Prefab.GetComponent<ConsumableItem>().Consume(_item.Data);                
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            _item.Data.InvokeDropEvent();
        }
    }
}