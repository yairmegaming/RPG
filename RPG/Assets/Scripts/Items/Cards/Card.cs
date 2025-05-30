using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    [Header("Card Info")]
    public string cardID;
    public string cardName;
    public Sprite cardImage;
    public string cardDescription;
    public ItemRarity itemRarity;

    [Header("Card Effect")]
    public CardEffectType effectType; // BuffSelf or DebuffEnemy
    public CardStatType statType;     // Attack, Defense, Health
    public float effectMultiplier = 1.1f; // 1.1 = +10%, 0.9 = -10%
    public int buffDuration = 1; // In turns, max 5

    [Header("Usage")]
    public bool isConsumable = true; // Always true for cards
}

public enum CardEffectType { BuffSelf, DebuffEnemy }
public enum CardStatType { Attack, Defense, Health }