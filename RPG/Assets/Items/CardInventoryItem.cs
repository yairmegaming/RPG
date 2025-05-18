using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image cardImage;
    public CanvasGroup canvasGroup { get; private set; }
    public Card myCard { get; private set; }
    public CardInventorySlot activeSlot { get; set; }

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(Card card, CardInventorySlot parent)
    {
        myCard = card;
        activeSlot = parent;
        activeSlot.myCardItem = this;
        cardImage.sprite = myCard.cardImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CardInventory.Singleton.SetCarriedCard(this);
        }
    }
}