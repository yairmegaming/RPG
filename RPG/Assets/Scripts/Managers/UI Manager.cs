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
    [SerializeField] private GameObject winCombatMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject shopMenuUI;
    [SerializeField] private GameObject eventUI;

    [Header("Battle UI")]
    [SerializeField] private GameObject attackButtonsGroup; // <-- Use this instead of array
    [SerializeField] private GameObject[] menuButtons;
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyHealthBarText;
    [SerializeField] private GameObject playerHealthBarText;
    [SerializeField] private GameObject combatText;

    private PlayerManager playerManager;
    private BattleManager battleManager;
    private EnemyManager enemyManager;

    private Dictionary<UIEnum, GameObject> uiPanels;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        battleManager = FindObjectOfType<BattleManager>();

        uiPanels = new Dictionary<UIEnum, GameObject>
        {
            { UIEnum.MainMenu, mainMenuUI },
            { UIEnum.Settings, settingsUI },
            { UIEnum.WinCombat, winCombatMenuUI },
            { UIEnum.GameOver, gameOverUI },
            { UIEnum.Inventory, inventoryUI },
            { UIEnum.Battle, battleUI },
            { UIEnum.Map, mapUI },
            { UIEnum.Shop, shopMenuUI },
            { UIEnum.Event, eventUI }
        };
    }

    private void Start()
    {
        SetActiveUI(UIEnum.MainMenu);
    }

    private void Update()
    {
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

    public void SetActiveUI(UIEnum uiEnum)
    {
        foreach (var panel in uiPanels)
        {
            if (panel.Value != null)
                panel.Value.SetActive(panel.Key == uiEnum);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    // --- UI ELEMENT HELPERS ---

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
        if (attackButtonsGroup != null)
            attackButtonsGroup.SetActive(isActive);

        if (menuButtons != null)
        {
            foreach (var button in menuButtons)
            {
                if (button != null)
                    button.SetActive(!isActive);
            }
        }
    }

    // --- SHOP/INVENTORY EXAMPLE ---

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

    // Call this to show the battle UI and hide menus/inventory
    public void ShowBattleUI()
    {
        battleUI.SetActive(true);
        settingsUI.SetActive(false);
        inventoryUI.SetActive(false);
        attackButtonsGroup.SetActive(false);
    }

    // Call this to open the settings (menu)
    public void ShowSettingsMenu()
    {
        battleUI.SetActive(false);
        settingsUI.SetActive(true);
        inventoryUI.SetActive(false);
        attackButtonsGroup.SetActive(false);
    }

    // Call this to open the card inventory
    public void ShowCardInventory()
    {
        battleUI.SetActive(false);
        settingsUI.SetActive(false);
        inventoryUI.SetActive(true);
        attackButtonsGroup.SetActive(false);
    }

    // Call this to show attack buttons (and hide others if needed)
    public void ShowAttackButtons()
    {
        battleUI.SetActive(true);
        settingsUI.SetActive(false);
        inventoryUI.SetActive(false);
        attackButtonsGroup.SetActive(true);
    }

    // Optional: Hide all overlays (for transitions, etc.)
    public void HideAllMenus()
    {
        battleUI.SetActive(false);
        settingsUI.SetActive(false);
        inventoryUI.SetActive(false);
        attackButtonsGroup.SetActive(false);
    }
}
