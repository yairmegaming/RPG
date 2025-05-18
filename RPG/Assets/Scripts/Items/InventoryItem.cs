using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.EventSystems;


public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image itemImage;
    public CanvasGroup canvasGroup { get; private set; }
    public Item myItem { get; private set; }
    public InventorySlot activeSlot { get; set; }

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        myItem = item;
        activeSlot = parent;
        activeSlot.myItem = this;
        itemImage.sprite = myItem.itemImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}
