using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIManager : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject battleUI;

    [Header("Battle UI")]
    [SerializeField] private GameObject attackButtonsGroup; // <-- Use this instead of array
    [SerializeField] private GameObject[] combatMenuButtons;
    [SerializeField] private GameObject playerHealthBarText;
    [SerializeField] private GameObject combatText;

    [Header("Enemy Combat UI")]
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyHealthBarText;
    [SerializeField] private GameObject enemyChoiceRockImage;
    [SerializeField] private GameObject enemyChoicePaperImage;
    [SerializeField] private GameObject enemyChoiceScissorsImage;

    [Header("Inventory UI")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cardInventoryUI;
    [SerializeField] private GameObject itemInventoryUI;

    [Header("Combat Win UI")]
    [SerializeField] private GameObject winCombatMenuUI;
    [SerializeField] private GameObject shopMenuUI;

    [Header("Combat Lose UI")]
    [SerializeField] private GameObject loseCombatMenuUI;
    [SerializeField] private GameObject goldEarnedText;
    [SerializeField] private GameObject totalBattleWonText;

    [Header("Shop UI")]
    [SerializeField] private GameObject shopItemOne;
    [SerializeField] private GameObject shopItemTwo;
    [SerializeField] private GameObject shopItemThree;

    [Header("Pause Menu UI")]
    [SerializeField] private GameObject pauseMenuUI;

    [Header("Event UI (Not Used Yet)")]
    [SerializeField] private GameObject eventUI;


    private PlayerManager playerManager;
    private BattleManager battleManager;
    private EnemyManager enemyManager;

    private Dictionary<UIEnum, GameObject> uiPanels;

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
            SetEnemySprite(enemyManager.EnemyPrefab.GetComponent<SpriteRenderer>().sprite);
            SetCombatText("Player's Turn");
        }

        StartCoroutine(CombatTextUpdate());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
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

    // Example for UI panels
    public void SetMainMenuActive()
    {
        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);
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
        RandomizeShopItems();
    }
    public void SetShopMenuActive()
    {
        if (shopMenuUI != null) shopMenuUI.SetActive(true);
        if (winCombatMenuUI != null) winCombatMenuUI.SetActive(false);
        if (battleUI != null) battleUI.SetActive(false);
        UpdateShopUI();
    }
    public void SetInventoryCardActive()
    {
        if (cardInventoryUI != null) cardInventoryUI.SetActive(true);
        if (itemInventoryUI != null) itemInventoryUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OnResetButtonPressed()
    {
        playerManager.ResetPlayer();
        enemyManager.ResetEnemy();
        UpdateStatsUI();
        SetMainMenuActive();
    }

    public void OnStartNewBattle()
    {
        playerManager.StartNewBattle();
        enemyManager.ResetEnemy();
        SetBattleActive();
        UpdateStatsUI();
    }

    public void SetEventActive()
    {
        if (eventUI != null) eventUI.SetActive(false);
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

    // List of all possible shop items (assign in Inspector or populate at runtime)
    [SerializeField] private List<Item> allShopItems = new List<Item>();

    // Array to hold the 3 items available to buy this round
    [SerializeField] private Item[] availableShopItems = new Item[3];

    // Call this after winning a round to randomize the shop items
    public void RandomizeShopItems()
    {
        List<Item> pool = new List<Item>(allShopItems);
        for (int i = 0; i < availableShopItems.Length; i++)
        {
            if (pool.Count == 0) break;
            int randomIndex = Random.Range(0, pool.Count);
            availableShopItems[i] = pool[randomIndex];
            pool.RemoveAt(randomIndex);
        }
        UpdateShopUI();
    }

    // Call this from each of the 3 shop buttons, passing 0, 1, or 2
    public void BuyShopItem(int shopSlot)
    {
        if (shopSlot < 0 || shopSlot >= availableShopItems.Length)
        {
            Debug.Log("Invalid shop slot.");
            return;
        }
        Item item = availableShopItems[shopSlot];
        BuyItem(item);
    }

    // Update the shop UI to show the 3 available items
    private void UpdateShopUI()
    {
        GameObject[] shopDisplays = new GameObject[] { shopItemOne, shopItemTwo, shopItemThree };

        for (int i = 0; i < shopDisplays.Length; i++)
        {
            if (i < availableShopItems.Length && availableShopItems[i] != null && shopDisplays[i] != null)
            {
                var displayImage = shopDisplays[i].GetComponent<UnityEngine.UI.Image>();
                if (displayImage != null)
                    displayImage.sprite = availableShopItems[i].itemImage; // Assuming your Item has a 'itemImage' field

                shopDisplays[i].SetActive(true);
            }
            else if (shopDisplays[i] != null)
            {
                shopDisplays[i].SetActive(false);
            }
        }
    }

    // Update BuyItem to accept Item instead of GameObject
    private void BuyItem(Item item)
    {
        if (item == null) return;

        // Check if the player can afford the item
        if (playerManager.PlayerGold < item.itemValue)
        {
            Debug.Log("Not enough gold to buy this item.");
            return;
        }

        // Deduct the cost from the player's gold
        playerManager.PlayerGold -= item.itemValue;

        // Add the item to the player's inventory
        playerManager.AddItemToInventory(item);

        // Optionally, you can also update the UI or provide feedback to the player
        Debug.Log("Bought item: " + item.itemName);
    }

// Simple pause menu toggle
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

// Update stats UI (call after gold/battle win changes)
public void UpdateStatsUI()
{
    if (goldEarnedText != null && playerManager != null)
        goldEarnedText.GetComponent<UnityEngine.UI.Text>().text = "Gold Earned: " + playerManager.totalGoldEarned;
    if (totalBattleWonText != null && playerManager != null)
        totalBattleWonText.GetComponent<UnityEngine.UI.Text>().text = "Battles Won: " + playerManager.totalBattlesWon;
}
}
