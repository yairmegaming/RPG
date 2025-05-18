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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

}
