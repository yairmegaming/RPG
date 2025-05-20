using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject winCombatUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject eventUI;

    [Header("Battle UI")]
    [SerializeField] private GameObject[] attackButtons;
    [SerializeField] private GameObject[] menuButtons;
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyHealthBarText;
    [SerializeField] private GameObject playerHealthBarText;
    [SerializeField] private GameObject combatText;

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
        // Set the initial UI state
        SetActiveUI(UIEnum.MainMenu);
    }

    private void Update()
    {
        // Update UI elements based on game state
        if (playerManager != null && enemyManager != null)
        {
            SetEnemyHealthBarText(enemyManager.EnemyHealth.ToString());
            SetPlayerHealthBarText(playerManager.CurrentHealth.ToString());
            SetCombatText("Player's Turn");
            SetEnemySprite(enemyManager.EnemyPrefab.GetComponent<SpriteRenderer>().sprite);
        }

        StartCoroutine(CombatTextUpdate());
    }

    private IEnumerator CombatTextUpdate()
    {
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
    }
    // --- UI BUTTON FUNCTIONS ---

    public void OpenSettingsMenu()
    {
        SetActiveUI(UIEnum.Settings);
    }

    public void CloseSettingsMenu()
    {
        SetActiveUI(UIEnum.MainMenu);
    }

    public void OpenGameOverMenu()
    {
        SetActiveUI(UIEnum.GameOver);
    }

    public void OpenInventoryMenu()
    {
        SetActiveUI(UIEnum.Inventory);
    }

    public void OpenBattleMenu()
    {
        SetActiveUI(UIEnum.BattleUI);
    }

    public void OpenMapMenu()
    {
        SetActiveUI(UIEnum.MapUI);
    }

    public void OpenShopMenu()
    {
        SetActiveUI(UIEnum.ShopUI);
    }

    public void OpenEventMenu()
    {
        SetActiveUI(UIEnum.EventUI);
    }

    public void GoToNextFight()
    {
        SetActiveUI(UIEnum.BattleUI);
        enemyManager.SpawnRandomEnemy();
        playerManager.PlayerState = PlayerStates.PlayerChoosing;
        // Optionally reset player/enemy health, update UI, etc.
        Debug.Log("Next fight started!");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    // --- END UI BUTTON FUNCTIONS ---

    public void SetActiveUI(UIEnum uiEnum)
    {
        // Logic to set the active UI based on the enum value
        // This could involve enabling/disabling GameObjects, changing scenes, etc.
        switch (uiEnum)
        {
            case UIEnum.MainMenu:
                // Activate Main Menu UI
                mainMenuUI.SetActive(true);
                settingsUI.SetActive(false);
                break;
            case UIEnum.Settings:
                // Activate Settings UI
                mainMenuUI.SetActive(false);
                settingsUI.SetActive(true);
                break;
            case UIEnum.GameOver:
                // Activate Game Over UI
                battleUI.SetActive(false);
                gameOverUI.SetActive(true);
                break;
            case UIEnum.WinCombatMenu:
                // Activate Win Combat Menu UI
                battleUI.SetActive(false);
                winCombatUI.SetActive(true);
                break;
            case UIEnum.Inventory:
                // Activate Inventory UI
                break;
            case UIEnum.BattleUI:
                // Activate Battle UI
                break;
            case UIEnum.MapUI:
                // Activate Map UI
                break;
            case UIEnum.ShopUI:
                // Activate Shop UI
                break;
            case UIEnum.EventUI:
                // Activate Event UI
                battleUI.SetActive(false);
                eventUI.SetActive(true);
                break;
        }
    }

    private void SetEnemyHealthBarText(string text)
    {
        enemyHealthBarText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    private void SetPlayerHealthBarText(string text)
    {
        playerHealthBarText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    private void SetCombatText(string text)
    {
        combatText.GetComponent<UnityEngine.UI.Text>().text = text;
    }
    private void SetEnemySprite(Sprite sprite)
    {
        enemySprite.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }
    private void SetAttackButtonsActive(bool isActive)
    {
        foreach (var button in attackButtons)
        {
            button.SetActive(isActive);
        }

        foreach (var button in menuButtons)
        {
            button.SetActive(!isActive);
        }
    }

    private void BuyItem(GameObject item)
    {
        if (item == null) 
            {
                Debug.Log("Item is Sold.");
                return;
            }
        if (playerManager.inventory.Count >= playerManager.inventorySize)
            {
                Debug.Log("Inventory is full.");
                return;
            }
        if (item.GetComponent<Item>().itemValue > playerManager.PlayerGold)
            {
                Debug.Log("Not enough Gold to buy this item.");
                return;
            }
        playerManager.PlayerGold -= item.GetComponent<Item>().itemValue;
        playerManager.inventory.Add(item.GetComponent<Item>());
        item.SetActive(false);
    }

}
