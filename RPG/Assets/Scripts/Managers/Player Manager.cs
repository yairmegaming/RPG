using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerManager : MonoBehaviour
{
    [Header("Equipped Items")]
    public Item equippedNecklace;
    public Item equippedRing;
    public Item equippedAmulet;

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Player Combat")]
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private int baseArmourPenetration = 0;
    [SerializeField] private int baseDefense = 0;
    [SerializeField] private int baseMaxHealth = 3;

    [Header("Player Gold")]
    [SerializeField] private int playerGold = 0;

    [Header("Player Items")]
    public List<Item> inventory = new List<Item>();
    public int inventorySize = 20;

    [Header("Player Stats Tracking")]
    public int totalGoldEarned = 0;
    public int totalBattlesWon = 0;

    private PlayerStates playerState;
    private PlayerChoiceEnum playerChoice;
    private int currentHealth;

    private PlayerCombat PlayerCombat => player != null ? player.GetComponent<PlayerCombat>() : null;

    public int ModifiedDamage => baseDamage +
        (equippedNecklace ? equippedNecklace.itemDamage : 0) +
        (equippedRing ? equippedRing.itemDamage : 0) +
        (equippedAmulet ? equippedAmulet.itemDamage : 0);

    public int ModifiedDefense => baseDefense +
        (equippedNecklace ? equippedNecklace.itemDefense : 0) +
        (equippedRing ? equippedRing.itemDefense : 0) +
        (equippedAmulet ? equippedAmulet.itemDefense : 0);

    public int ModifiedMaxHealth => baseMaxHealth +
        (equippedNecklace ? equippedNecklace.itemMaxHealth : 0) +
        (equippedRing ? equippedRing.itemMaxHealth : 0) +
        (equippedAmulet ? equippedAmulet.itemMaxHealth : 0);

    public int ModifiedArmourPenetration => baseArmourPenetration +
        (equippedNecklace ? equippedNecklace.itemArmourPenetration : 0) +
        (equippedRing ? equippedRing.itemArmourPenetration : 0) +
        (equippedAmulet ? equippedAmulet.itemArmourPenetration : 0);

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, ModifiedMaxHealth);
    }

    public int PlayerGold
    {
        get => playerGold;
        set => playerGold = value;
    }

    public PlayerStates PlayerState
    {
        get => playerState;
        set => playerState = value;
    }

    public PlayerChoiceEnum PlayerChoice
    {
        get => playerChoice;
        set => playerChoice = value;
    }

    private void InitializePlayer()
    {
        CurrentHealth = ModifiedMaxHealth;
        PlayerCombat.UpdatePlayerStats();
        PlayerChoice = PlayerChoiceEnum.none;
        PlayerState = PlayerStates.PlayerChoosing;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            PlayerState = PlayerStates.PlayerDied;
        }
    }

    public void TakeDamage(int damage)
    {
        if (PlayerCombat != null)
        {
            PlayerCombat.TakeDamage(damage);
            CurrentHealth = PlayerCombat.CurrentHealth;
        }
    }

    public void Heal(int healAmount)
    {
        if (PlayerCombat != null)
        {
            PlayerCombat.Heal(healAmount);
            CurrentHealth = PlayerCombat.CurrentHealth;
        }
    }

    public void Defend(int damage)
    {
        if (PlayerCombat != null)
            PlayerCombat.OnDefense(damage);
    }

    public void MakeChoice(PlayerChoiceEnum choice)
    {
        switch (choice)
        {
            case PlayerChoiceEnum.Rock:
                PlayerCombat.RockChoice();
                break;
            case PlayerChoiceEnum.Paper:
                PlayerCombat.PaperChoice();
                break;
            case PlayerChoiceEnum.Scissors:
                PlayerCombat.ScissorsChoice();
                break;
            default:
                PlayerCombat.ResetChoices();
                break;
        }
    }

    // Inventory management
    public void AddItemToInventory(Item item)
    {
        if (item == null || inventory.Count >= inventorySize)
        {
            Debug.LogWarning("Cannot add item to inventory.");
            return;
        }
        inventory.Add(item);
        Debug.Log($"Added {item.itemName} to inventory.");
    }

    public void RemoveItemFromInventory(Item item)
    {
        if (item == null || !inventory.Contains(item))
        {
            Debug.LogWarning("Cannot remove item from inventory.");
            return;
        }
        inventory.Remove(item);
        Debug.Log($"Removed {item.itemName} from inventory.");
    }

    public void EquipItem(Item item)
    {
        if (item == null) return;
        switch (item.itemClass)
        {
            case SlotTag.Necklace:
                equippedNecklace = item;
                break;
            case SlotTag.Ring:
                equippedRing = item;
                break;
            case SlotTag.Amulet:
                equippedAmulet = item;
                break;
            default:
                Debug.LogWarning("Invalid item type for equipping.");
                return;
        }
        UpdatePlayerStats();
        Debug.Log($"Equipped {item.itemName}.");
    }

    public void UnequipItem(SlotTag slot)
    {
        switch (slot)
        {
            case SlotTag.Necklace:
                equippedNecklace = null;
                break;
            case SlotTag.Ring:
                equippedRing = null;
                break;
            case SlotTag.Amulet:
                equippedAmulet = null;
                break;
            default:
                Debug.LogWarning("Invalid slot for unequipping.");
                return;
        }
        UpdatePlayerStats();
        Debug.Log($"Unequipped item from {slot} slot.");
    }

    public void UpdatePlayerStats()
    {
        PlayerCombat.UpdatePlayerStats();
    }

    // Call this whenever the player receives gold (from any source)
    public void AddGold(int amount)
    {
        PlayerGold += amount;
        totalGoldEarned += amount;
        Debug.Log($"Player received {amount} gold. Total gold earned: {totalGoldEarned}");
    }

    // Call this whenever the player wins a battle
    public void RegisterBattleWin()
    {
        totalBattlesWon++;
        Debug.Log($"Player has won {totalBattlesWon} battles.");
    }

    // Call this at the start of a new battle to ensure tracking is correct
    public void StartNewBattle()
    {
        // Any per-battle resets can go here if needed
        Debug.Log("New battle started. Tracking continues.");
    }

    // Call this when the reset button is pressed to reset all stats and progress
    public void ResetPlayer()
    {
        CurrentHealth = ModifiedMaxHealth;
        PlayerGold = 0;
        inventory.Clear();
        totalGoldEarned = 0;
        totalBattlesWon = 0;
        Debug.Log("Player reset. All stats and progress cleared.");
    }

    //public void ApplyDebuff(CardStatType statType, float multiplier)
    //{
    //    switch (statType)
    //    {
    //        case CardStatType.Attack:
    //            ModifiedDamage = Mathf.RoundToInt(ModifiedDamage * multiplier);
    //            break;
    //        case CardStatType.Defense:
    //            ModifiedDefense = Mathf.RoundToInt(ModifiedDefense * multiplier);
    //            break;
    //        case CardStatType.Health:
    //            CurrentHealth = Mathf.RoundToInt(CurrentHealth * multiplier);
    //            break;
    //    }
    //    // Optionally, add logic to reset these at the end of the turn
    //}

    private List<BuffEffect> activeBuffs = new List<BuffEffect>();

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
        // Replace _modifiedDamage/_modifiedDefense with your actual stat logic
        // Example:
        // _modifiedDamage = Mathf.RoundToInt(baseDamage * attackMult);
        // _modifiedDefense = Mathf.RoundToInt(baseDefense * defenseMult);
        // CurrentHealth = Mathf.Min(Mathf.RoundToInt(baseMaxHealth * healthMult), ModifiedMaxHealth);
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

    // Example: call this at the start of each player turn
}
