#if ENABLE_CARDS
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInventory : MonoBehaviour
{
    public static CardInventory Singleton { get; private set; }
    public CardInventoryItem carriedCard;

    [SerializeField] private CardInventorySlot[] cardSlots;
    [SerializeField] private Transform draggableTransform;
    [SerializeField] private CardInventoryItem cardItemPrefab;
    [SerializeField] private Card[] cards;

    [Header("Player Reference")]
    [SerializeField] private PlayerManager playerManager;

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
    }

    public void SpawnCard(Card card = null)
    {
        Card _card = card ?? cards[Random.Range(0, cards.Length)];
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].myCardItem == null)
            {
                Instantiate(cardItemPrefab, cardSlots[i].transform).Initialize(_card, cardSlots[i]);
                playerManager?.AddCardToDeck(_card);
                break;
            }
        }
    }

    private void Update()
    {
        if (carriedCard == null) return;
        carriedCard.transform.position = Input.mousePosition;
    }

    public void SetCarriedCard(CardInventoryItem cardItem)
    {
        carriedCard = cardItem;
        carriedCard.canvasGroup.blocksRaycasts = false;
        cardItem.transform.SetParent(draggableTransform);
    }

    public void ResetAllCardsForNewCombat()
    {
        foreach (var slot in cardSlots)
        {
            if (slot.myCardItem != null)
                slot.myCardItem.ResetForNewCombat();
        }
    }
}
#endif