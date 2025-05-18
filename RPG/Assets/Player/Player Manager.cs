using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
     [Header("Inventory")]
    public List<Item> inventory = new List<Item>();

    [Header("Equipped Items")]
    public Item equippedNecklace;
    public Item equippedRing;
    public Item equippedAmulet;

    [Header("Player Visuals")]
    [SerializeField] private GameObject player;

    [Header("Player Combat")]
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private int baseArmourPenetration = 0;
    [SerializeField] private int baseDefense = 0;
    [SerializeField] private int baseMaxHealth = 3;

    [Header("Player Score")]
    [SerializeField] private int playerScore = 0;

    [Header("Player Items")]
    [SerializeField] private GameObject necklace;
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject amulet;

    [Header("Player Cards")]
    public List<Card> cardDeck = new List<Card>();


    private PlayerStates playerState;
    private PlayerChoiceEnum playerChoice;

    private int currentHealth;

    private PlayerCombat PlayerCombat => player.GetComponent<PlayerCombat>();

    private Item EquippedNecklace => necklace.GetComponent<Item>();
    private Item EquippedRing => ring.GetComponent<Item>();
    private Item EquippedAmulet => amulet.GetComponent<Item>();

    public int ModifiedDamage => baseDamage + EquippedNecklace.itemDamage + EquippedRing.itemDamage + EquippedAmulet.itemDamage;
    public int ModifiedDefense => baseDefense + EquippedNecklace.itemDefense + EquippedRing.itemDefense + EquippedAmulet.itemDefense;
    public int ModifiedMaxHealth => baseMaxHealth + EquippedNecklace.itemMaxHealth + EquippedRing.itemMaxHealth + EquippedAmulet.itemMaxHealth;
    public int ModifiedArmourPenetration => baseArmourPenetration + EquippedNecklace.itemArmourPenetration + EquippedRing.itemArmourPenetration + EquippedAmulet.itemArmourPenetration;

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, ModifiedMaxHealth);
    }

    public int PlayerScore
    {
        get => playerScore;
        set => playerScore = value;
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

    private void Awake()
    {
        InitializePlayer();
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

    public void AddItemToInventory(Item item)
    {
        inventory.Add(item);
        Debug.Log($"Added {item.itemName} to inventory.");
    }

    public void EquipItem(Item item)
    {
        switch (item.itemType)
        {
            case "Necklace":
                equippedNecklace = item;
                break;
            case "Ring":
                equippedRing = item;
                break;
            case "Amulet":
                equippedAmulet = item;
                break;
            default:
                Debug.LogWarning("Invalid item type for equipping.");
                return;
        }

        UpdatePlayerStats();
        Debug.Log($"Equipped {item.itemName}.");
    }

    public void UnequipItem(string slot)
    {
        switch (slot)
        {
            case "Necklace":
                equippedNecklace = null;
                break;
            case "Ring":
                equippedRing = null;
                break;
            case "Amulet":
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
        // Update stats based on equipped items
        PlayerCombat.UpdatePlayerStats();
    }

    public void AddCardToDeck(Card card)
    {
        cardDeck.Add(card);
        Debug.Log($"Added {card.cardName} to deck.");
    }

    public void UseCard(Card card)
    {
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
        cardDeck.Remove(card);
        Debug.Log($"Used card: {card.cardName}");
    }
}
