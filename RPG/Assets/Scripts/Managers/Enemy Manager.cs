using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemyPrefab;
    private EnemyState enemyState;
    private EnemyChoiceEnum enemyChoiceEnum;
    private EnemyDefault enemyDefault;

    private PlayerManager playerManager;
    private EnemyManager enemyManager;

    private Dictionary<EnemyRarity, int> rarityWeights = new Dictionary<EnemyRarity, int>
    {
        { EnemyRarity.Common, 60 },
        { EnemyRarity.Uncommon, 25 },
        { EnemyRarity.Rare, 10 },
        { EnemyRarity.Epic, 4 },
        { EnemyRarity.Legendary, 1 }
    };

    public EnemyState EnemyState
    {
        get => enemyState;
        set => enemyState = value;
    }
    public EnemyChoiceEnum EnemyChoice
    {
        get => enemyChoiceEnum;
        set => enemyChoiceEnum = value;
    }

    public int EnemyDamage
    {
        get => enemyDefault.EnemyDamage;
        set => enemyDefault.EnemyDamage = value;
    }

    public int EnemyHealth
    {
        get => enemyDefault.EnemyCurrentHealth;
        set => enemyDefault.EnemyCurrentHealth = value;
    }

    public int EnemyDefense
    {
        get => enemyDefault.EnemyCurrentDefense;
        set => enemyDefault.EnemyCurrentDefense = value;
    }

    public int EnemyScoreWorth
    {
        get => enemyDefault.EnemyCurrentScoreWorth;
        set => enemyDefault.EnemyCurrentScoreWorth = value;
    }

    public Sprite EnemySprite
    {
        get => enemyDefault.enemySprite;
        set => enemyDefault.enemySprite = value;
    }
    public GameObject EnemyPrefab
    {
        get => enemyPrefab;
        set => enemyPrefab = value;
    }

    private void Awake()
    {
        if (playerManager == null)
            playerManager = FindObjectOfType<PlayerManager>();
        if (enemyManager == null)
            enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void Start()
    {
        SpawnRandomEnemy();

        EnemyChoice = EnemyChoiceEnum.none;
        EnemyState = EnemyState.none;
    }

    private void Update()
    {
        if (enemyPrefab == null) return;

        enemyDefault = enemyPrefab.GetComponent<EnemyDefault>();

        switch (EnemyState)
        {
            case EnemyState.EnemyChoosing:
                enemyDefault.EnemyChoosing();
                break;
            case EnemyState.EnemyAttack:
                enemyDefault.EnemyAttackAnimation();
                break;
            case EnemyState.EnemyDefend:
                enemyDefault.EnemyDefendAnimation();
                break;
            case EnemyState.EnemyDamaged:
                enemyDefault.EnemyDamagedAnimation();
                break;
            case EnemyState.EnemyDied:
                enemyDefault.EnemyDeathAnimation();
                break;
        }
    }

    public void SpawnRandomEnemy()
    {
        // Destroy previous enemy if it exists
        if (enemyPrefab != null)
            Destroy(enemyPrefab);

        // Get all enemy data assets
        var allEnemies = GameDatabase.Instance.allEnemiesData; // List<EnemyDefaultData>
        if (allEnemies == null || allEnemies.Count == 0)
        {
            Debug.LogWarning("No enemies found in GameDatabase!");
            return;
        }

        // Get player win count (adjust this if your variable is named differently)
        int battlesWon = 0;
        if (playerManager != null)
            battlesWon = playerManager.totalBattlesWon;

        // Use dynamic weights
        var dynamicWeights = RarityWeightManager.GetEnemyRarityWeights(battlesWon);

        // Build weighted pool
        List<EnemyDefaultData> weightedPool = new List<EnemyDefaultData>();
        foreach (var enemy in allEnemies)
        {
            int weight = dynamicWeights.ContainsKey(enemy.rarity) ? dynamicWeights[enemy.rarity] : 1;
            for (int i = 0; i < weight; i++)
                weightedPool.Add(enemy);
        }

        if (weightedPool.Count == 0) return;
        EnemyDefaultData selectedData = weightedPool[Random.Range(0, weightedPool.Count)];

        // Instantiate a prefab and assign stats from selectedData
        enemyPrefab = Instantiate(GameDatabase.Instance.enemyPrefab, transform.position, Quaternion.identity, transform);
        enemyDefault = enemyPrefab.GetComponent<EnemyDefault>();
        enemyDefault.enemySprite = selectedData.enemySprite;
        enemyDefault.EnemyDamage = selectedData.EnemyDamage;
        enemyDefault.EnemyHealth = selectedData.EnemyHealth;
        enemyDefault.EnemyDefense = selectedData.EnemyDefense;
        enemyDefault.EnemyScoreWorth = selectedData.EnemyScoreWorth;
        enemyDefault.rockChance = selectedData.rockChance;
        enemyDefault.paperChance = selectedData.paperChance;
        enemyDefault.scissorsChance = selectedData.scissorsChance;

        enemyDefault.enemyCards.Clear();
        enemyDefault.ResetEnemyCardsForCombat();

        var allCards = GameDatabase.Instance.allCards; // List<Card>
        if (allCards != null && allCards.Count > 0)
        {
            // Only allow cards of same or lower rarity
            var allowedCards = allCards.FindAll(card => (int)card.itemRarity <= (int)selectedData.rarity);

            // The higher the rarity, the more cards (e.g., Common:1, Uncommon:2, Rare:3, Epic:4, Legendary:5)
            int cardsToAssign = Mathf.Clamp((int)selectedData.rarity + 1, 1, 5);

            // Randomly assign cards
            for (int i = 0; i < cardsToAssign && allowedCards.Count > 0; i++)
            {
                int idx = Random.Range(0, allowedCards.Count);
                enemyDefault.enemyCards.Add(allowedCards[idx]);
                allowedCards.RemoveAt(idx);
            }
        }

        enemyPrefab.tag = "Enemy";
    }

    public void ResetEnemy()
    {
        if (enemyPrefab != null)
        {
            Destroy(enemyPrefab);
            enemyPrefab = null;
        }
        SpawnRandomEnemy();
    }
}

