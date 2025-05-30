using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    [Header("Enemy Visuals")]
    public Sprite enemySprite;

    [Header("Enemy Manager")]
    public GameObject EnemyManager;
    protected EnemyManager enemyManagerScript;

    [Header("Enemy Combat")]
    public int EnemyDamage = 1;
    public int EnemyHealth = 3;
    public int EnemyDefense = 0;

    [Header("Enemy Score")]
    public int EnemyScoreWorth = 0;

    [Header("Enemy Animation (Optional)")]
    public Animator enemyAnimator;
    public RuntimeAnimatorController enemyAnimatorController;

    [Header("Enemy Choice Chances (Sum should be > 0)")]
    [Range(0, 100)] public int rockChance = 33;
    [Range(0, 100)] public int paperChance = 33;
    [Range(0, 100)] public int scissorsChance = 34;

    [Header("Enemy Cards")]
    public List<Card> enemyCards = new List<Card>();
    private HashSet<Card> usedCardsThisCombat = new HashSet<Card>();

    // Buff tracking
    private float attackBuffMultiplier = 1f;
    private float defenseBuffMultiplier = 1f;
    private float healthBuffMultiplier = 1f;

    private int baseDamage;
    private int baseDefense;
    private int baseHealth;

    public int EnemyCurrentDamage { get; set; }
    public int EnemyCurrentDefense { get; set; }
    public int EnemyCurrentHealth { get; set; }
    public int EnemyCurrentScoreWorth { get; set; }

    private List<BuffEffect> activeBuffs = new List<BuffEffect>();

    private void Awake()
    {
        if (EnemyManager != null)
            enemyManagerScript = EnemyManager.GetComponent<EnemyManager>();
    }

    private void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        // Store base stats for resetting buffs
        baseDamage = EnemyDamage;
        baseDefense = EnemyDefense;
        baseHealth = EnemyHealth;
        if (enemyManagerScript != null)
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.none;
        EnemyCurrentHealth = EnemyHealth;
        EnemyCurrentDamage = EnemyDamage;
        EnemyCurrentDefense = EnemyDefense;
        EnemyCurrentScoreWorth = EnemyScoreWorth;
        if (enemyAnimator != null && enemyAnimatorController != null)
            enemyAnimator.runtimeAnimatorController = enemyAnimatorController;
        ResetBuffs();
    }

    public void TakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {EnemyCurrentHealth}");
    }

    public void Heal(int healAmount)
    {
        EnemyCurrentHealth = Mathf.Min(EnemyCurrentHealth + healAmount, EnemyHealth);
        Debug.Log($"Enemy healed {healAmount} health. Current health: {EnemyCurrentHealth}");
    }

    public void OnDefense(int damage, int armourPenetration = 0)
    {
        int effectiveDamage = Mathf.Max(1, damage - (EnemyCurrentDefense - armourPenetration));
        TakeDamage(effectiveDamage);
        Debug.Log($"Enemy defended with {EnemyCurrentDefense} defense. Damage taken: {effectiveDamage}");
    }

    // Set the chance for each choice in the Inspector (rockChance, paperChance, scissorsChance)
    public virtual void EnemyChoosing()
    {
        int total = rockChance + paperChance + scissorsChance;
        if (total <= 0)
        {
            Debug.LogWarning("Enemy choice chances are all zero! Defaulting to Rock.");
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Rock;
            return;
        }

        int roll = Random.Range(1, total + 1);
        if (roll <= rockChance)
        {
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Rock;
        }
        else if (roll <= rockChance + paperChance)
        {
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Paper;
        }
        else
        {
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Scissors;
        }
    }

    // Animation methods are now safe to call even if no Animator is assigned
    public void EnemyAttackAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Attack");
    }
    public void EnemyDefendAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Defend");
    }
    public void EnemyDamagedAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Damaged");
    }
    public void EnemyDeathAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Died");
    }

    public void ResetEnemyCardsForCombat()
    {
        usedCardsThisCombat.Clear();
        ResetBuffs();
    }

    public bool CanUseCard(Card card)
    {
        return card != null && !usedCardsThisCombat.Contains(card);
    }

    public void MarkCardUsed(Card card)
    {
        if (card != null)
            usedCardsThisCombat.Add(card);
    }

    public void ResetBuffs()
    {
        attackBuffMultiplier = 1f;
        defenseBuffMultiplier = 1f;
        healthBuffMultiplier = 1f;
        EnemyCurrentDamage = baseDamage;
        EnemyCurrentDefense = baseDefense;
        EnemyCurrentHealth = Mathf.Min(EnemyCurrentHealth, baseHealth); // Don't overheal
    }

    // Call this at the start of each turn if buffs are one-turn only
    public void OnTurnStart()
    {
        ResetBuffs();
    }

    // --- Card Play Logic ---
    public void PlayAvailableCards(PlayerManager playerManager)
    {
        foreach (var card in enemyCards)
        {
            if (CanUseCard(card))
            {
                ApplyCardEffect(card, playerManager);
                MarkCardUsed(card);
                // If you want only one card per turn, break here
                break;
            }
        }
    }

    private void ApplyCardEffect(Card card, PlayerManager playerManager)
    {
        float multiplier = card.effectMultiplier;
        switch (card.effectType)
        {
            case CardEffectType.BuffSelf:
                ApplyBuff(card.statType, multiplier);
                break;
            case CardEffectType.DebuffEnemy:
                if (playerManager != null)
                    playerManager.ApplyDebuff(card.statType, multiplier);
                break;
        }
        Debug.Log($"Enemy used card: {card.cardName} ({card.effectType} {card.statType} x{multiplier})");
    }

    private void ApplyBuff(CardStatType statType, float multiplier)
    {
        switch (statType)
        {
            case CardStatType.Attack:
                attackBuffMultiplier *= multiplier;
                EnemyCurrentDamage = Mathf.RoundToInt(baseDamage * attackBuffMultiplier);
                break;
            case CardStatType.Defense:
                defenseBuffMultiplier *= multiplier;
                EnemyCurrentDefense = Mathf.RoundToInt(baseDefense * defenseBuffMultiplier);
                break;
            case CardStatType.Health:
                healthBuffMultiplier *= multiplier;
                int newHealth = Mathf.RoundToInt(baseHealth * healthBuffMultiplier);
                // Only heal if buff increases health
                if (newHealth > EnemyCurrentHealth)
                    EnemyCurrentHealth = newHealth;
                break;
        }
    }

    public void ApplyBuffOrDebuff(BuffTargetStat stat, float multiplier, int turns)
    {
        turns = Mathf.Min(turns, 5);
        activeBuffs.Add(new BuffEffect(stat, multiplier, turns));
        UpdateStatsWithBuffs();
    }

    public void UpdateStatsWithBuffs()
    {
        float attackMult = 1f, defenseMult = 1f, healthMult = 1f;
        foreach (var buff in activeBuffs)
        {
            switch (buff.stat)
            {
                case BuffTargetStat.Attack: attackMult *= buff.multiplier; break;
                case BuffTargetStat.Defense: defenseMult *= buff.multiplier; break;
                case BuffTargetStat.Health: healthMult *= buff.multiplier; break;
            }
        }
        // Replace EnemyDamage/EnemyDefense/EnemyHealth with your actual stat logic
        // Example:
        // EnemyDamage = Mathf.RoundToInt(baseDamage * attackMult);
        // EnemyDefense = Mathf.RoundToInt(baseDefense * defenseMult);
        // EnemyHealth = Mathf.Min(Mathf.RoundToInt(baseHealth * healthMult), EnemyHealth);
    }

    public void TickBuffs()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            activeBuffs[i].turnsLeft--;
            if (activeBuffs[i].turnsLeft <= 0)
                activeBuffs.RemoveAt(i);
        }
        UpdateStatsWithBuffs();
    }

    // Example: call this at the start of each enemy turn
}
