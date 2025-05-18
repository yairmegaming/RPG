using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Player Reference")]
    [SerializeField] private PlayerManager playerManager;

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
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].myItem == null)
            {
                Instantiate(itemPrefab, equipmentSlots[i].transform).Initialize(_item, equipmentSlots[i]);
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
            if (item.activeSlot.itemClass != SlotTag.None && item.myItem.itemClass != item.activeSlot.itemClass) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.itemClass != SlotTag.None)
        {
            EquipEquipment(item.activeSlot.itemClass, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggableTransform);
    }

    public void EquipEquipment(SlotTag itemClass, InventoryItem item)
    {
        // Update equipment slot
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].itemClass == itemClass)
            {
                equipmentSlots[i].myItem = item;
                break;
            }
        }
        // Notify PlayerManager
        playerManager?.EquipItem(item?.myItem);
        Debug.Log($"Equipped {item?.myItem?.itemName ?? "None"} in slot {itemClass}");
    }
}
