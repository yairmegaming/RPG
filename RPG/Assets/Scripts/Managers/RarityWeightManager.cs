using System.Collections.Generic;

public static class RarityWeightManager
{
    public static Dictionary<ItemRarity, int> GetItemRarityWeights(int battlesWon)
    {
        var weights = new Dictionary<ItemRarity, int>
        {
            { ItemRarity.Common, 60 },
            { ItemRarity.Uncommon, 25 },
            { ItemRarity.Rare, 10 },
            { ItemRarity.Epic, 4 },
            { ItemRarity.Legendary, 1 }
        };

        if (battlesWon >= 5)
        {
            weights[ItemRarity.Common] -= 15;
            weights[ItemRarity.Uncommon] += 5;
            weights[ItemRarity.Rare] += 5;
            weights[ItemRarity.Epic] += 4;
            weights[ItemRarity.Legendary] += 1;
        }
        if (battlesWon >= 10)
        {
            weights[ItemRarity.Common] -= 15;
            weights[ItemRarity.Uncommon] -= 5;
            weights[ItemRarity.Rare] += 10;
            weights[ItemRarity.Epic] += 10;
            weights[ItemRarity.Legendary] += 5;
        }
        if (battlesWon >= 20)
        {
            weights[ItemRarity.Common] = 5;
            weights[ItemRarity.Uncommon] = 10;
            weights[ItemRarity.Rare] = 25;
            weights[ItemRarity.Epic] = 30;
            weights[ItemRarity.Legendary] = 30;
        }

        foreach (var key in weights.Keys)
            if (weights[key] < 1) weights[key] = 1;

        return weights;
    }

    public static Dictionary<EnemyRarity, int> GetEnemyRarityWeights(int battlesWon)
    {
        var weights = new Dictionary<EnemyRarity, int>
        {
            { EnemyRarity.Common, 60 },
            { EnemyRarity.Uncommon, 25 },
            { EnemyRarity.Rare, 10 },
            { EnemyRarity.Epic, 4 },
            { EnemyRarity.Legendary, 1 }
        };

        if (battlesWon >= 5)
        {
            weights[EnemyRarity.Common] -= 15;
            weights[EnemyRarity.Uncommon] += 5;
            weights[EnemyRarity.Rare] += 5;
            weights[EnemyRarity.Epic] += 4;
            weights[EnemyRarity.Legendary] += 1;
        }
        if (battlesWon >= 10)
        {
            weights[EnemyRarity.Common] -= 15;
            weights[EnemyRarity.Uncommon] -= 5;
            weights[EnemyRarity.Rare] += 10;
            weights[EnemyRarity.Epic] += 10;
            weights[EnemyRarity.Legendary] += 5;
        }
        if (battlesWon >= 20)
        {
            weights[EnemyRarity.Common] = 5;
            weights[EnemyRarity.Uncommon] = 10;
            weights[EnemyRarity.Rare] = 25;
            weights[EnemyRarity.Epic] = 30;
            weights[EnemyRarity.Legendary] = 30;
        }

        foreach (var key in weights.Keys)
            if (weights[key] < 1) weights[key] = 1;

        return weights;
    }
}