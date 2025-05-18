using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject eventUI;

    [Header("Battle UI")]
    [SerializeField] private GameObject attackButtons;
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyHealthBarText;
    [SerializeField] private GameObject playerHealthBarText;
    [SerializeField] private GameObject combatText;

    private PlayerManager playerManager;
    private EnemyManager enemyManager;

    private void Awake()
    {
        // Automatically find the first PlayerManager and EnemyManager in the scene
        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    public void SetActiveUI(UIEnum uiEnum)
    {
        // Logic to set the active UI based on the enum value
        // This could involve enabling/disabling GameObjects, changing scenes, etc.
        switch (uiEnum)
        {
            case UIEnum.MainMenu:
                // Activate Main Menu UI
                break;
            case UIEnum.Settings:
                // Activate Settings UI
                break;
            case UIEnum.GameOver:
                // Activate Game Over UI
                break;
            case UIEnum.WinCombatMenu:
                // Activate Win Combat Menu UI
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
        attackButtons.SetActive(isActive);
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
