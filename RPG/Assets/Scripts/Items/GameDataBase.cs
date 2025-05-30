using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatabase")]
public class GameDatabase : ScriptableObject
{
    [Header("Items")]
    public List<Item> allItems = new List<Item>();

    [Header("Cards")]
    public List<Card> allCards = new List<Card>();

    [Header("Enemies")]
    public List<EnemyDefaultData> allEnemiesData = new List<EnemyDefaultData>();

    [Header("Enemy Prefab (for runtime instantiation)")]
    public GameObject enemyPrefab;

    // Singleton pattern for easy access
    private static GameDatabase _instance;
    public static GameDatabase Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<GameDatabase>("GameDatabase");
            return _instance;
        }
    }

    // Example lookup methods
    public Item GetItemByID(string id)
    {
        return allItems.Find(item => item.itemID == id);
    }

    public Card GetCardByID(string id)
    {
        return allCards.Find(card => card.cardID == id);
    }

    public EnemyDefaultData GetEnemyByID(string id)
    {
        return allEnemiesData.Find(enemy => enemy.enemyID == id);
    }
}