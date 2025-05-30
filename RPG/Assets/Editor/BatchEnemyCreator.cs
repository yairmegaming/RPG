using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class BatchEnemyCreator
{
    [MenuItem("Tools/Batch Create 10 Enemies With Cards")]
    public static void CreateEnemies()
    {
        string folderPath = "Assets/Enemies";
        Directory.CreateDirectory(folderPath);

        // Load all cards in the project
        string[] cardGuids = AssetDatabase.FindAssets("t:Card", new[] { "Assets/Cards" });
        List<Card> allCards = new List<Card>();
        foreach (var guid in cardGuids)
        {
            var card = AssetDatabase.LoadAssetAtPath<Card>(AssetDatabase.GUIDToAssetPath(guid));
            if (card != null) allCards.Add(card);
        }

        System.Array rarities = System.Enum.GetValues(typeof(EnemyRarity));
        for (int i = 1; i <= 10; i++)
        {
            EnemyDefaultData enemy = ScriptableObject.CreateInstance<EnemyDefaultData>();
            enemy.enemyID = $"enemy_{i}";
            enemy.enemyName = $"Enemy {i}";
            enemy.rarity = (EnemyRarity)rarities.GetValue(Random.Range(0, rarities.Length));

            // Set stats based on rarity (as before)
            var rewardRange = GetRewardRangeForRarity(enemy.rarity);
            var healthRange = GetHealthRangeForRarity(enemy.rarity);
            var damageRange = GetDamageRangeForRarity(enemy.rarity);
            var defenseRange = GetDefenseRangeForRarity(enemy.rarity);

            enemy.EnemyScoreWorth = Random.Range(rewardRange.min, rewardRange.max + 1);
            enemy.EnemyHealth = Random.Range(healthRange.min, healthRange.max + 1);
            enemy.EnemyDamage = Random.Range(damageRange.min, damageRange.max + 1);
            enemy.EnemyDefense = Random.Range(defenseRange.min, defenseRange.max + 1);

            enemy.spawnChance = Random.Range(1, 101);
            enemy.rockChance = Random.Range(0, 101);
            enemy.paperChance = Random.Range(0, 101 - enemy.rockChance);
            enemy.scissorsChance = 100 - enemy.rockChance - enemy.paperChance;

            // Assign cards: only cards of same or lower rarity
            var allowedCards = allCards.FindAll(card => (int)card.itemRarity <= (int)enemy.rarity);
            int cardsToAssign = Mathf.Clamp((int)enemy.rarity + 1, 1, 5);
            enemy.enemyCards = new List<Card>();
            var tempCards = new List<Card>(allowedCards);
            for (int c = 0; c < cardsToAssign && tempCards.Count > 0; c++)
            {
                int idx = Random.Range(0, tempCards.Count);
                enemy.enemyCards.Add(tempCards[idx]);
                tempCards.RemoveAt(idx);
            }

            AssetDatabase.CreateAsset(enemy, $"{folderPath}/Enemy_{i}.asset");
        }
        AssetDatabase.SaveAssets();
        Debug.Log("Created 10 enemies with cards!");
    }

    private static (int min, int max) GetRewardRangeForRarity(EnemyRarity rarity)
    {
        switch (rarity)
        {
            case EnemyRarity.Common: return (3, 8);
            case EnemyRarity.Uncommon: return (9, 15);
            case EnemyRarity.Rare: return (16, 25);
            case EnemyRarity.Epic: return (26, 40);
            case EnemyRarity.Legendary: return (41, 60);
            default: return (3, 8);
        }
    }

    private static (int min, int max) GetHealthRangeForRarity(EnemyRarity rarity)
    {
        switch (rarity)
        {
            case EnemyRarity.Common: return (5, 12);
            case EnemyRarity.Uncommon: return (13, 20);
            case EnemyRarity.Rare: return (21, 30);
            case EnemyRarity.Epic: return (31, 45);
            case EnemyRarity.Legendary: return (46, 60);
            default: return (5, 12);
        }
    }

    private static (int min, int max) GetDamageRangeForRarity(EnemyRarity rarity)
    {
        switch (rarity)
        {
            case EnemyRarity.Common: return (1, 3);
            case EnemyRarity.Uncommon: return (3, 5);
            case EnemyRarity.Rare: return (5, 8);
            case EnemyRarity.Epic: return (8, 12);
            case EnemyRarity.Legendary: return (12, 18);
            default: return (1, 3);
        }
    }

    private static (int min, int max) GetDefenseRangeForRarity(EnemyRarity rarity)
    {
        switch (rarity)
        {
            case EnemyRarity.Common: return (0, 1);
            case EnemyRarity.Uncommon: return (1, 2);
            case EnemyRarity.Rare: return (2, 4);
            case EnemyRarity.Epic: return (4, 7);
            case EnemyRarity.Legendary: return (7, 10);
            default: return (0, 1);
        }
    }
}