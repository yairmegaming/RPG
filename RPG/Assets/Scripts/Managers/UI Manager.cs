using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cardInventoryUI;
    [SerializeField] private GameObject itemInventoryUI;
    [SerializeField] private GameObject winCombatMenuUI;
    [SerializeField] private GameObject shopMenuUI;
    [SerializeField] private GameObject loseCombatMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject eventUI; // Not used yet

    [Header("Battle UI")]
    [SerializeField] private GameObject attackButtonsGroup;
    [SerializeField] private GameObject[] combatMenuButtons;
    [SerializeField] private GameObject playerHealthBarText;
    [SerializeField] private GameObject combatText;

    [Header("Enemy Combat UI")]
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyHealthBarText;
    [SerializeField] private GameObject enemyChoiceRockImage;
    [SerializeField] private GameObject enemyChoicePaperImage;
    [SerializeField] private GameObject enemyChoiceScissorsImage;

    [Header("Stats UI")]
    [SerializeField] private GameObject goldEarnedText;
    [SerializeField] private GameObject totalBattleWonText;

    [Header("References")]
    [SerializeField] private ShopManager shopManager;

    private PlayerManager playerManager;
    private BattleManager battleManager;
    private EnemyManager enemyManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void Start()
    {
        SetMainMenuActive();
    }

    private void Update()
    {
        if (playerManager != null && enemyManager != null)
        {
            SetEnemyHealthBarText(enemyManager.EnemyHealth.ToString());
            SetPlayerHealthBarText(playerManager.CurrentHealth.ToString());
            if (enemyManager.EnemyPrefab != null)
            {
                var sr = enemyManager.EnemyPrefab.GetComponent<SpriteRenderer>();
                if (sr != null)
                    SetEnemySprite(sr.sprite);
            }
            SetCombatText("Player's Turn");
        }

        if (!isCombatTextUpdating)
            StartCoroutine(CombatTextUpdate());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private bool isCombatTextUpdating = false;
    private IEnumerator CombatTextUpdate()
    {
        isCombatTextUpdating = true;
        if (battleManager != null)
        {
            switch (battleManager.BattleState)
            {
                case BattleEnum.ChoosingAction:
                    SetAttackButtonsActive(true);
                    SetCombatText("Player's Turn");
                    break;
                case BattleEnum.CombatEffects:
                    SetAttackButtonsActive(false);
                    SetCombatText("Enemy's Turn");
                    yield return new WaitForSeconds(1f);
                    break;
                case BattleEnum.CombatResolution:
                    SetAttackButtonsActive(false);
                    SetCombatText("Resolving Combat...");
                    yield return new WaitForSeconds(1f);
                    break;
                case BattleEnum.CombatEnd:
                    SetAttackButtonsActive(false);
                    SetCombatText("Battle Ended");
                    yield return new WaitForSeconds(1f);
                    break;
            }
        }
        isCombatTextUpdating = false;
    }

    // --- UI PANEL FUNCTIONS ---

    public void SetMainMenuActive()
    {
        if (mainMenuUI != null) mainMenuUI.SetActive(true);
    }

    public void SetSettingsActive()
    {
        if (settingsUI != null) settingsUI.SetActive(true);
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
    }

    public void SetLoseCombatMenuActive()
    {
        if (loseCombatMenuUI != null) loseCombatMenuUI.SetActive(true);
        if (battleUI != null) battleUI.SetActive(false);
        if (inventoryUI != null) inventoryUI.SetActive(false);
        if (goldEarnedText != null && playerManager != null)
            goldEarnedText.GetComponent<UnityEngine.UI.Text>().text = "Gold Earned: " + playerManager.PlayerGold;
    }

    public void SetInventoryActive()
    {
        if (inventoryUI != null) inventoryUI.SetActive(true);
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
    }

    public void SetBattleActive()
    {
        if (battleUI != null) battleUI.SetActive(true);
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
        if (inventoryUI != null) inventoryUI.SetActive(false);
        if (loseCombatMenuUI != null) loseCombatMenuUI.SetActive(false);
    }

    public void SetWinCombatMenuActive()
    {
        if (winCombatMenuUI != null) winCombatMenuUI.SetActive(true);
        if (battleUI != null) battleUI.SetActive(false);
        if (shopMenuUI != null) shopMenuUI.SetActive(false);
        if (shopManager != null) shopManager.RandomizeShopItems();
    }

    public void SetShopMenuActive()
    {
        if (shopMenuUI != null) shopMenuUI.SetActive(true);
        if (winCombatMenuUI != null) winCombatMenuUI.SetActive(false);
        if (battleUI != null) battleUI.SetActive(false);
        if (shopManager != null) shopManager.UpdateShopUI();
    }

    public void SetInventoryCardActive()
    {
        if (cardInventoryUI != null) cardInventoryUI.SetActive(true);
        if (itemInventoryUI != null) itemInventoryUI.SetActive(false);
    }

    public void SetEventActive()
    {
        if (eventUI != null) eventUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OnResetButtonPressed()
    {
        if (playerManager != null) playerManager.ResetPlayer();
        if (enemyManager != null) enemyManager.ResetEnemy();
        UpdateStatsUI();
        SetMainMenuActive();
    }

    public void OnStartNewBattle()
    {
        if (playerManager != null) playerManager.StartNewBattle();
        if (enemyManager != null) enemyManager.ResetEnemy();
        SetBattleActive();
        UpdateStatsUI();
    }

    // --- UI ELEMENT HELPERS ---

    private void SetEnemyHealthBarText(string text)
    {
        if (enemyHealthBarText != null)
            enemyHealthBarText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    private void SetPlayerHealthBarText(string text)
    {
        if (playerHealthBarText != null)
            playerHealthBarText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    private void SetCombatText(string text)
    {
        if (combatText != null)
            combatText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    private void SetEnemySprite(Sprite sprite)
    {
        if (enemySprite != null)
            enemySprite.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }
    private void SetAttackButtonsActive(bool isActive)
    {
        if (attackButtonsGroup != null)
            attackButtonsGroup.SetActive(isActive);

        if (combatMenuButtons != null)
        {
            foreach (var button in combatMenuButtons)
            {
                if (button != null)
                    button.SetActive(!isActive);
            }
        }
    }

    // --- STATS UI ---

    public void UpdateStatsUI()
    {
        if (goldEarnedText != null && playerManager != null)
            goldEarnedText.GetComponent<UnityEngine.UI.Text>().text = "Gold Earned: " + playerManager.totalGoldEarned;
        if (totalBattleWonText != null && playerManager != null)
            totalBattleWonText.GetComponent<UnityEngine.UI.Text>().text = "Battles Won: " + playerManager.totalBattlesWon;
    }

    // --- PAUSE MENU ---

    public void TogglePauseMenu()
    {
        bool isPaused = Time.timeScale == 0;
        if (isPaused)
        {
            Time.timeScale = 1;
            if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
        }
    }
}
