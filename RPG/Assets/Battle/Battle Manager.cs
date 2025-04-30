using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Managers")]
    public GameObject playerManagerObject;
    public GameObject enemyManagerObject;

    [Header("Battle Units")]
    public GameObject player;
    public GameObject enemy;

    private PlayerManager playerManagerScript;
    private EnemyManager enemyManagerScript;

    private BattleEnum battleState;

    private void Start()
    {
        playerManagerScript = playerManagerObject.GetComponent<PlayerManager>();
        enemyManagerScript = enemyManagerObject.GetComponent<EnemyManager>();

        StartBattle();
    }

    private void Update()
    {
        switch (battleState)
        {
            case BattleEnum.ChoosingAction:
                // Wait for the player to make a choice
                break;

            case BattleEnum.CombatEffects:
                ResolveCombat();
                break;

            case BattleEnum.CombatResolution:
                CheckBattleOutcome();
                break;

            case BattleEnum.CombatEnd:
                EndBattle();
                break;
        }
    }

    private void StartBattle()
    {
        battleState = BattleEnum.ChoosingAction;
        Debug.Log("Battle started!");
    }

    public void PlayerAction(PlayerChoiceEnum playerChoice)
    {
        playerManagerScript.MakeChoice(playerChoice);
        enemyManagerScript.EnemyState = EnemyState.EnemyChoosing;
        enemyManagerScript.enemyPrefab.GetComponent<EnemyDefault>().EnemyChoosing();

        battleState = BattleEnum.CombatEffects;
    }

    private void ResolveCombat()
    {
        PlayerChoiceEnum playerChoice = playerManagerScript.PlayerChoice;
        EnemyChoiceEnum enemyChoice = enemyManagerScript.EnemyChoice;

        if (playerChoice == PlayerChoiceEnum.none || enemyChoice == EnemyChoiceEnum.none)
        {
            Debug.LogWarning("Invalid choices detected!");
            return;
        }

        // Rock-Paper-Scissors logic
        if ((int)playerChoice == (int)enemyChoice)
        {
            Debug.Log("It's a tie!");
        }
        else if ((playerChoice == PlayerChoiceEnum.Rock && enemyChoice == EnemyChoiceEnum.Scissors) ||
                 (playerChoice == PlayerChoiceEnum.Paper && enemyChoice == EnemyChoiceEnum.Rock) ||
                 (playerChoice == PlayerChoiceEnum.Scissors && enemyChoice == EnemyChoiceEnum.Paper))
        {
            Debug.Log("Player wins this round!");
            enemyManagerScript.EnemyHealth -= playerManagerScript.ModifiedDamage;
        }
        else
        {
            Debug.Log("Enemy wins this round!");
            playerManagerScript.TakeDamage(enemyManagerScript.EnemyDamage);
        }

        battleState = BattleEnum.CombatResolution;
    }

    private void CheckBattleOutcome()
    {
        if (playerManagerScript.CurrentHealth <= 0)
        {
            Debug.Log("Player has been defeated!");
            battleState = BattleEnum.CombatEnd;
        }
        else if (enemyManagerScript.EnemyHealth <= 0)
        {
            Debug.Log("Enemy has been defeated!");
            battleState = BattleEnum.CombatEnd;
        }
        else
        {
            battleState = BattleEnum.ChoosingAction;
        }
    }

    private void EndBattle()
    {
        Debug.Log("Battle ended!");
        // Handle post-battle logic (e.g., rewards, returning to the overworld)
    }
}
