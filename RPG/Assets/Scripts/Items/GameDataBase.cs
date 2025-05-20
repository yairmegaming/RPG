using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameDatabase")]
public class GameDatabase : ScriptableObject
{
    [Header("All Items and Cards")]
    public List<Item> allItems = new List<Item>();
    public List<Card> allCards = new List<Card>();

    [Header("All Enemy Prefabs")]
    public List<GameObject> allEnemies = new List<GameObject>();

    private Dictionary<string, Item> itemDict;
    private Dictionary<string, Card> cardDict;
    private Dictionary<string, GameObject> enemyDict;

    private static GameDatabase _instance;
    public static GameDatabase Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.Load<GameDatabase>("GameDatabase");
            return _instance;
        }
    }

    private void OnEnable()
    {
        BuildDictionaries();
    }

    private void BuildDictionaries()
    {
        itemDict = new Dictionary<string, Item>(allItems.Count);
        foreach (var item in allItems)
        {
            if (item != null && !string.IsNullOrEmpty(item.itemID))
                itemDict[item.itemID] = item;
        }

        cardDict = new Dictionary<string, Card>(allCards.Count);
        foreach (var card in allCards)
        {
            if (card != null && !string.IsNullOrEmpty(card.cardID))
                cardDict[card.cardID] = card;
        }

        enemyDict = new Dictionary<string, GameObject>(allEnemies.Count);
        foreach (var enemy in allEnemies)
        {
            if (enemy != null)
                enemyDict[enemy.name] = enemy;
        }
    }

    public Item GetItemByID(string id)
    {
        if (itemDict == null || itemDict.Count != allItems.Count)
            BuildDictionaries();
        itemDict.TryGetValue(id, out var item);
        return item;
    }

    public Card GetCardByID(string id)
    {
        if (cardDict == null || cardDict.Count != allCards.Count)
            BuildDictionaries();
        cardDict.TryGetValue(id, out var card);
        return card;
    }

    public GameObject GetEnemyByName(string name)
    {
        if (enemyDict == null || enemyDict.Count != allEnemies.Count)
            BuildDictionaries();
        enemyDict.TryGetValue(name, out var enemy);
        return enemy;
    }
}