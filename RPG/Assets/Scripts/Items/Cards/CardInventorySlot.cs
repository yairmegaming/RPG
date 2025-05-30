using UnityEngine;
using UnityEngine.EventSystems;

public class CardInventorySlot : MonoBehaviour, IPointerClickHandler
{
    public CardInventoryItem myCardItem { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (CardInventory.Singleton.carriedCard == null) return;
            SetCard(CardInventory.Singleton.carriedCard);
        }
    }

    public void SetCard(CardInventoryItem cardItem)
    {
        CardInventory.Singleton.carriedCard = null;
        myCardItem = cardItem;
        myCardItem.activeSlot = this;
        myCardItem.transform.SetParent(transform);
        myCardItem.canvasGroup.blocksRaycasts = true;
    }
}