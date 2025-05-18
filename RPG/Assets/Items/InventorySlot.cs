using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }
    public SloTag itemClass;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.Singleton.carriedItem == null) return;
            if (itemClass != SloTag.None && Inventory.Singleton.carriedItem.myItem.itemClass != itemClass) return;
            SetItem(Inventory.Singleton.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.Singleton.carriedItem = null;

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (itemClass != SloTag.None)
        {
            Inventory.Singleton.EquipEquipment(itemClass, myItem);
        }
    }
}

