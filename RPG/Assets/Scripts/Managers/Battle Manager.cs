using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Units")]
    public GameObject player;
    public GameObject enemy;

    private PlayerManager playerManagerScript;
    private EnemyManager enemyManagerScript;
    private UIManager uiManager;

    private BattleEnum battleState;

    public BattleEnum BattleState
    {
        get => battleState; private set => battleState = value;
    }

    private void Awake()
    {
        if (playerManagerScript == null)
            playerManagerScript = FindObjectOfType<PlayerManager>();
        if (enemyManagerScript == null)
            enemyManagerScript = FindObjectOfType<EnemyManager>();
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        StartBattle();
    }

    private void Update()
    {
        StartCoroutine(CombatTextUpdate());
    }

    private IEnumerator CombatTextUpdate()
    {
        switch (BattleState)
        {
            case BattleEnum.ChoosingAction:
                // Wait for the player to make a choice
                break;
            case BattleEnum.CombatEffects:
                yield return new WaitForSeconds(1f);
                ResolveCombat();
                break;

            case BattleEnum.CombatResolution:
                yield return new WaitForSeconds(1f);
                CheckBattleOutcome();
                break;

            case BattleEnum.CombatEnd:
                yield return new WaitForSeconds(1f);
                EndBattle();
                break;
        }
    }
    private void StartBattle()
    {
        BattleState = BattleEnum.ChoosingAction;
        Debug.Log("Battle started!");
    }

    public void PlayerRock()
    {
        PlayerAction(PlayerChoiceEnum.Rock);
    }
    public void PlayerPaper()
    {
        PlayerAction(PlayerChoiceEnum.Paper);
    }
    public void PlayerScissors()
    {
        PlayerAction(PlayerChoiceEnum.Scissors);
    }

    public void PlayerAction(PlayerChoiceEnum playerChoice)
    {
        playerManagerScript.MakeChoice(playerChoice);
        enemyManagerScript.EnemyState = EnemyState.EnemyChoosing;
        enemyManagerScript.EnemyPrefab.GetComponent<EnemyDefault>().EnemyChoosing();

        BattleState = BattleEnum.CombatEffects;
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

        BattleState = BattleEnum.CombatResolution;
    }

    private void CheckBattleOutcome()
    {
        if (playerManagerScript.CurrentHealth <= 0)
        {
            Debug.Log("Player has been defeated!");
            BattleState = BattleEnum.CombatEnd;
        }
        else if (enemyManagerScript.EnemyHealth <= 0)
        {
            Debug.Log("Enemy has been defeated!");
            BattleState = BattleEnum.CombatEnd;
        }
        else
        {
            BattleState = BattleEnum.ChoosingAction;
        }
    }

    private void EndBattle()
    {
        if (playerManagerScript == null || enemyManagerScript == null || uiManager == null)
            return;

        if (playerManagerScript.CurrentHealth > 0 && enemyManagerScript.EnemyHealth <= 0)
        {
            playerManagerScript.RegisterBattleWin();
            int rewardGold = enemyManagerScript.EnemyScoreWorth;
            playerManagerScript.AddGold(rewardGold);
            uiManager.SetWinCombatMenuActive();
            uiManager.UpdateStatsUI();
        }
        else if (playerManagerScript.CurrentHealth <= 0)
        {
            uiManager.SetLoseCombatMenuActive();
            uiManager.UpdateStatsUI();
        }
    }
}