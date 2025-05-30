using System.Collections.Generic;
using UnityEngine;

public class InventorySaveLoad : MonoBehaviour
{
    public PlayerManager playerManager;

    public void SaveInventory()
    {
        if (playerManager == null) return;

        List<string> itemIDs = new List<string>();
        foreach (var item in playerManager.inventory)
        {
            itemIDs.Add(item.itemID);
        }

        PlayerPrefs.SetString("Inventory", string.Join(",", itemIDs));
        PlayerPrefs.SetString("EquippedNecklace", playerManager.equippedNecklace?.itemID ?? "");
        PlayerPrefs.SetString("EquippedRing", playerManager.equippedRing?.itemID ?? "");
        PlayerPrefs.SetString("EquippedAmulet", playerManager.equippedAmulet?.itemID ?? "");
        PlayerPrefs.Save();

        Debug.Log("Inventory saved.");
    }

    public void LoadInventory()
    {
        if (playerManager == null) return;

        string inventoryData = PlayerPrefs.GetString("Inventory", "");
        string[] itemIDs = inventoryData.Split(',');

        foreach (var id in itemIDs)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Item item = FindItemByID(id);
                if (item != null)
                {
                    playerManager.AddItemToInventory(item);
                }
            }
        }

        playerManager.equippedNecklace = FindItemByID(PlayerPrefs.GetString("EquippedNecklace", ""));
        playerManager.equippedRing = FindItemByID(PlayerPrefs.GetString("EquippedRing", ""));
        playerManager.equippedAmulet = FindItemByID(PlayerPrefs.GetString("EquippedAmulet", ""));

        playerManager.UpdatePlayerStats();
        Debug.Log("Inventory loaded.");
    }

    private Item FindItemByID(string id)
    {
        // Implement logic to find an item by its ID (e.g., from a database or list of all items)
        return null;
    }
}