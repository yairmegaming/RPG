using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Manager")]
    [SerializeField] private GameObject playerManagerObject;

    private PlayerManager PlayerManager => playerManagerObject.GetComponent<PlayerManager>();

    private int currentDamage;
    private int currentMaxHealth;
    private int currentHealth;
    private int currentDefense;
    private int currentArmourPenetration;

    private const float DefaultHealEffectiveness = 100f;
    private const float DefenseEffectiveness = 2f;
    private const int CriticalHealthPercentage = 10;

    private void Start()
    {
        UpdatePlayerStats();
        PlayerManager.PlayerChoice = PlayerChoiceEnum.none;
    }

    public void UpdatePlayerStats()
    {
        currentDamage = PlayerManager.ModifiedDamage;
        currentMaxHealth = PlayerManager.ModifiedMaxHealth;
        currentDefense = PlayerManager.ModifiedDefense;
        currentArmourPenetration = PlayerManager.ModifiedArmourPenetration;
        currentHealth = currentMaxHealth;
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, currentMaxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = damage >= currentHealth
            ? Mathf.CeilToInt(currentMaxHealth * CriticalHealthPercentage / 100f)
            : currentHealth - damage;

        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");
    }

    public void Heal(int healAmount, float healEffectiveness = DefaultHealEffectiveness)
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += Mathf.CeilToInt(healAmount * healEffectiveness / 100f);
            currentHealth = Mathf.Min(currentHealth, currentMaxHealth);
            Debug.Log($"Player healed {healAmount} health. Current health: {currentHealth}");
        }
        else
        {
            Debug.Log("Player is already at max health.");
        }
    }

    public void OnDefense(int enemyDamage, float defenseEffectiveness = DefenseEffectiveness)
    {
        if (enemyDamage > currentDefense)
        {
            int damageTaken = Mathf.CeilToInt((enemyDamage - currentDefense) / defenseEffectiveness);
            TakeDamage(damageTaken);
        }
        else
        {
            Debug.Log("Player defended against the attack. No damage taken.");
        }
    }

    public void RockChoice() => SetPlayerChoice(PlayerChoiceEnum.Rock, "Rock");
    public void PaperChoice() => SetPlayerChoice(PlayerChoiceEnum.Paper, "Paper");
    public void ScissorsChoice() => SetPlayerChoice(PlayerChoiceEnum.Scissors, "Scissors");
    public void ResetChoices() => SetPlayerChoice(PlayerChoiceEnum.none, "reset");

    private void SetPlayerChoice(PlayerChoiceEnum choice, string choiceName)
    {
        PlayerManager.PlayerChoice = choice;
        Debug.Log($"Player chose {choiceName}");
    }
}