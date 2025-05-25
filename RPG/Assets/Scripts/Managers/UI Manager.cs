using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIEnum
{
    MainMenu,
    Settings,
    WinCombat,
    GameOver,
    Inventory,
    Battle,
    Map,
    Shop,
    Event,
}

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

    private Dictionary<UIEnum, GameObject> uiPanels;
    private Dictionary<string, GameObject[]> uiGroups;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        battleManager = FindObjectOfType<BattleManager>();

        // Example: Assign tags to attack buttons if not already set
        var attackButtonsInScene = GameObject.FindGameObjectsWithTag("Untagged");
        foreach (var go in attackButtonsInScene)
        {
            if (go.name.Contains("AttackButton")) // or any naming convention you use
                go.tag = "AttackButton";
        }

        // Do the same for menu buttons if needed
        var menuButtonsInScene = GameObject.FindGameObjectsWithTag("Untagged");
        foreach (var go in menuButtonsInScene)
        {
            if (go.name.Contains("MenuButton"))
                go.tag = "MenuButton";
        }

        // Initialize the UI panel dictionary
        uiPanels = new Dictionary<UIEnum, GameObject>
        {
            { UIEnum.MainMenu, GameObject.Find("MainMenuUI") },
            { UIEnum.Settings, GameObject.Find("SettingsUI") },
            { UIEnum.WinCombat, GameObject.Find("WinCombatUI") },
            { UIEnum.GameOver, GameObject.Find("GameOverUI") },
            { UIEnum.Inventory, GameObject.Find("InventoryUI") },
            { UIEnum.Battle, GameObject.Find("BattleUI") },
            { UIEnum.Map, GameObject.Find("MapUI") },
            { UIEnum.Shop, GameObject.Find("ShopUI") },
            { UIEnum.Event, GameObject.Find("EventUI") }
        };

        // Example for button groups (using tags)
        uiGroups = new Dictionary<string, GameObject[]>
        {
            { "AttackButtons", GameObject.FindGameObjectsWithTag("AttackButton") },
            { "MenuButtons", GameObject.FindGameObjectsWithTag("MenuButton") }
        };

        if (!enemySprite) enemySprite = GameObject.Find("EnemySprite");
        if (!enemyHealthBarText) enemyHealthBarText = GameObject.Find("EnemyHealthBarText");
        if (!playerHealthBarText) playerHealthBarText = GameObject.Find("PlayerHealthBarText");
        if (!combatText) combatText = GameObject.Find("CombatText");
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
        SetActiveUI(UIEnum.Battle);
    }

    public void OpenMapMenu()
    {
        SetActiveUI(UIEnum.Map);
    }

    public void OpenShopMenu()
    {
        SetActiveUI(UIEnum.Shop);
    }

    public void OpenEventMenu()
    {
        SetActiveUI(UIEnum.Event);
    }

    public void StartNewGame()
    {
        // Logic to start a new game, reset player state, etc.
        playerManager.ResetPlayer();
        enemyManager.ResetEnemy();
        SetActiveUI(UIEnum.Battle);
        Debug.Log("New Game Started!");
    }

    public void OpenCombatChoice()
    {
        // Logic to open combat choice UI, e.g., showing attack options
        SetActiveUI(UIEnum.Battle);
        SetAttackButtonsActive(true);
        Debug.Log("Combat Choice Opened!");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    // --- END UI BUTTON FUNCTIONS ---

    public void SetActiveUI(UIEnum uiEnum)
    {
        foreach (var panel in uiPanels)
        {
            if (panel.Value != null)
                panel.Value.SetActive(panel.Key == uiEnum);
        }
    }

    public GameObject[] GetUIGroup(string groupName)
    {
        return uiGroups.TryGetValue(groupName, out var group) ? group : null;
    }

    public GameObject GetPanel(UIEnum uiEnum)
    {
        return uiPanels.TryGetValue(uiEnum, out var panel) ? panel : null;
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
