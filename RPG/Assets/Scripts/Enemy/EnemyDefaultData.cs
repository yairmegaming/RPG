using UnityEngine;

public enum EnemyRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyDefaultData : ScriptableObject
{
    public string enemyID;
    public string enemyName;
    public Sprite enemySprite;
    public int EnemyDamage = 1;
    public int EnemyHealth = 3;
    public int EnemyDefense = 0;
    public int EnemyScoreWorth = 0;
    public EnemyRarity rarity = EnemyRarity.Common;
    [Range(1, 100)]
    public int spawnChance = 50; // Optional: for custom weighting
    [Header("Enemy Choice Chances (Sum should be > 0)")]
    [Range(0, 100)] public int rockChance = 33;
    [Range(0, 100)] public int paperChance = 33;
    [Range(0, 100)] public int scissorsChance = 34;
}