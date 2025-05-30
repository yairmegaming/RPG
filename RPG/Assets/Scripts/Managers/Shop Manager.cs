using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    [SerializeField] private List<Item> allShopItems = new List<Item>();
    [SerializeField] private Item[] availableShopItems = new Item[3];

    [Header("Shop UI")]
    [SerializeField] private GameObject shopItemOne;
    [SerializeField] private GameObject shopItemTwo;
    [SerializeField] private GameObject shopItemThree;

    [Header("References")]
    [SerializeField] private PlayerManager playerManager;

    private Dictionary<ItemRarity, int> rarityWeights = new Dictionary<ItemRarity, int>
    {
        { ItemRarity.Common, 60 },
        { ItemRarity.Uncommon, 25 },
        { ItemRarity.Rare, 10 },
        { ItemRarity.Epic, 4 },
        { ItemRarity.Legendary, 1 }
    };

    public void RandomizeShopItems()
    {
        int battlesWon = 0;
        if (playerManager != null)
            battlesWon = playerManager.totalBattlesWon;

        var dynamicWeights = RarityWeightManager.GetItemRarityWeights(battlesWon);

        List<Item> weightedPool = new List<Item>();
        foreach (var item in allShopItems)
        {
            int weight = dynamicWeights.ContainsKey(item.itemRarity) ? dynamicWeights[item.itemRarity] : 1;
            for (int i = 0; i < weight; i++)
                weightedPool.Add(item);
        }

        for (int i = 0; i < availableShopItems.Length; i++)
        {
            if (weightedPool.Count == 0) break;
            int randomIndex = Random.Range(0, weightedPool.Count);
            Item selected = weightedPool[randomIndex];
            availableShopItems[i] = selected;
            weightedPool.RemoveAll(x => x == selected);
        }
        UpdateShopUI();
    }

    public void BuyShopItem(int shopSlot)
    {
        if (shopSlot < 0 || shopSlot >= availableShopItems.Length)
        {
            Debug.Log("Invalid shop slot.");
            return;
        }
        Item item = availableShopItems[shopSlot];
        BuyItem(item);
    }

    public void UpdateShopUI()
    {
        GameObject[] shopDisplays = new GameObject[] { shopItemOne, shopItemTwo, shopItemThree };
        for (int i = 0; i < shopDisplays.Length; i++)
        {
            if (i < availableShopItems.Length && availableShopItems[i] != null && shopDisplays[i] != null)
            {
                var displayImage = shopDisplays[i].GetComponent<UnityEngine.UI.Image>();
                if (displayImage != null)
                    displayImage.sprite = availableShopItems[i].itemImage;
                shopDisplays[i].SetActive(true);
            }
            else if (shopDisplays[i] != null)
            {
                shopDisplays[i].SetActive(false);
            }
        }
    }

    private void BuyItem(Item item)
    {
        if (item == null) return;
        if (playerManager == null) return;
        if (playerManager.PlayerGold < item.itemValue)
        {
            Debug.Log("Not enough gold to buy this item.");
            return;
        }
        playerManager.PlayerGold -= item.itemValue;
        playerManager.AddItemToInventory(item);
        Debug.Log("Bought item: " + item.itemName);
        UpdateShopUI();
    }

    private Dictionary<ItemRarity, int> GetDynamicRarityWeights(int battlesWon)
    {
        // Implement your dynamic weight calculation based on battles won
        // For now, return the default weights
        return rarityWeights;
    }
}