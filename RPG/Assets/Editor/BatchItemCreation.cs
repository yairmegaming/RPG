using UnityEngine;
using UnityEditor;
using System.IO;

public class BatchItemCreator
{
    [MenuItem("Tools/Batch Create 15 Items (Randomized)")]
    public static void CreateItems()
    {
        string[] types = { "Ring", "Amulet", "Necklace" };
        System.Array rarities = System.Enum.GetValues(typeof(ItemRarity));

        foreach (var type in types)
        {
            string folderPath = $"Assets/Prefabs/Items/Items/{type}s";
            Directory.CreateDirectory(folderPath); // Ensure folder exists

            for (int i = 1; i <= 5; i++)
            {
                Item item = ScriptableObject.CreateInstance<Item>();
                item.itemClass = (SlotTag)System.Enum.Parse(typeof(SlotTag), type);
                item.itemID = $"{type.ToLower()}_{i}";
                item.itemName = $"{type} {i}";
                item.itemRarity = (ItemRarity)rarities.GetValue(Random.Range(0, rarities.Length));
                // Set value based on rarity
                var valueRange = GetValueRangeForRarity(item.itemRarity);
                item.itemValue = Random.Range(valueRange.min, valueRange.max + 1);
                item.itemDamage = Random.Range(0, 6);
                item.itemDefense = Random.Range(0, 4);
                item.itemMaxHealth = Random.Range(0, 11);
                item.itemArmourPenetration = Random.Range(0, 3);
                item.itemDescription = $"A {item.itemRarity} {type.ToLower()} (auto-generated).";

                AssetDatabase.CreateAsset(item, $"{folderPath}/{type}_{i}.asset");
            }
        }
        AssetDatabase.SaveAssets();
        Debug.Log("Created 15 randomized items!");
    }

    private static (int min, int max) GetValueRangeForRarity(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return (5, 15);
            case ItemRarity.Uncommon: return (16, 30);
            case ItemRarity.Rare: return (31, 50);
            case ItemRarity.Epic: return (51, 80);
            case ItemRarity.Legendary: return (81, 120);
            default: return (5, 15);
        }
    }
}