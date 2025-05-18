using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton { get; private set; }
    public InventoryItem carriedItem;

    [Header("Equipment Inventory")]
    [SerializeField] private InventorySlot[] equipmentSlots;

    [SerializeField] private Transform draggableTransform;
    [SerializeField] private InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] private Item[] items;

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item ?? items[Random.Range(0, items.Length)];
        InventorySlot[] slots = equipmentSlots;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].myItem == null)
            {
                Instantiate(itemPrefab, slots[i].transform).Initialize(_item, slots[i]);
                break;
            }
        }
    }

    private void Update()
    {
        if (carriedItem == null) return;
        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.itemClass != SloTag.None && item.myItem.itemClass != item.activeSlot.itemClass) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.itemClass != SloTag.None)
        {
            EquipEquipment(item.activeSlot.itemClass, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggableTransform);
    }

    public void EquipEquipment(SloTag itemClass, InventoryItem item)
    {
        // Here you should call PlayerManager or another system to actually equip the item.
        // Example:
        // PlayerManager.Instance.EquipItem(item?.myItem, itemClass);
        // For now, just a debug log:
        Debug.Log($"Equipped {item?.myItem?.itemName ?? "None"} in slot {itemClass}");
    }
}
