using UnityEngine;
using UnityEngine.UI;

public class CardActionUI : MonoBehaviour
{
    public Button playButton;
    public Button cancelButton;
    private CardInventoryItem currentCard;
    private PlayerManager playerManager;
    private EnemyDefault enemyDefault;

    public static CardActionUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(CardInventoryItem card, PlayerManager player, EnemyDefault enemy)
    {
        currentCard = card;
        playerManager = player;
        enemyDefault = enemy;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentCard = null;
    }

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void OnPlayClicked()
    {
        if (currentCard != null)
            currentCard.PlayCard(playerManager, enemyDefault, true); // true = played by player
        Hide();
    }

    private void OnCancelClicked()
    {
        Hide();
    }
}