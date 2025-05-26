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

    [Header("Player Cards")]
    public List<Card> cardDeck = new List<Card>();
    public int maxDeckSize = 10;

    [Header("Player Stats Tracking")]
    public int totalGoldEarned = 0;
    public int totalBattlesWon = 0;

    private PlayerStates playerState;
    private PlayerChoiceEnum playerChoice;
    private int currentHealth;

    private PlayerCombat PlayerCombat => player.GetComponent<PlayerCombat>();

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
        PlayerCombat.TakeDamage(damage);
        CurrentHealth = PlayerCombat.CurrentHealth;
    }

    public void Heal(int healAmount)
    {
        PlayerCombat.Heal(healAmount);
        CurrentHealth = PlayerCombat.CurrentHealth;
    }

    public void Defend(int damage)
    {
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

    // Card management
    public void AddCardToDeck(Card card)
    {
        if (card == null || cardDeck.Count >= maxDeckSize)
        {
            Debug.LogWarning("Cannot add card to deck.");
            return;
        }
        cardDeck.Add(card);
        Debug.Log($"Added {card.cardName} to deck.");
    }

    public void RemoveCardFromDeck(Card card)
    {
        if (card == null || !cardDeck.Contains(card))
        {
            Debug.LogWarning("Cannot remove card from deck.");
            return;
        }
        cardDeck.Remove(card);
        Debug.Log($"Removed {card.cardName} from deck.");
    }

    public void UseCard(Card card)
    {
        if (card == null || !cardDeck.Contains(card))
        {
            Debug.LogWarning("Cannot use card.");
            return;
        }
        // Example: apply card effect
        switch (card.cardType)
        {
            case CardType.Attack:
                // Deal damage to enemy
                break;
            case CardType.Heal:
                Heal(card.cardPower);
                break;
            case CardType.Buff:
                // Apply buff
                break;
            case CardType.Debuff:
                // Apply debuff to enemy
                break;
        }
        RemoveCardFromDeck(card);
        Debug.Log($"Used card: {card.cardName}");
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
        cardDeck.Clear();
        totalGoldEarned = 0;
        totalBattlesWon = 0;
        Debug.Log("Player reset. All stats and progress cleared.");
    }
}
