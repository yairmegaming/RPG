using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInventoryItem : MonoBehaviour, IPointerClickHandler
{
    private Image cardImage;
    public CanvasGroup canvasGroup { get; private set; }
    public Card myCard { get; private set; }
    public CardInventorySlot activeSlot { get; set; }
    public bool usedThisCombat { get; private set; } = false;

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
        usedThisCombat = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !usedThisCombat)
        {
            // Show the card action UI instead of dragging
            var player = FindObjectOfType<PlayerManager>();
            var enemy = FindObjectOfType<EnemyDefault>();
            CardActionUI.Instance.Show(this, player, enemy);
        }
    }

    public void PlayCard(PlayerManager player, EnemyDefault enemy, bool playedByPlayer)
    {
        if (usedThisCombat || myCard == null) return;

        if (playedByPlayer)
        {
            if (myCard.effectType == CardEffectType.BuffSelf)
                player.ApplyBuffOrDebuff((BuffTargetStat)myCard.statType, myCard.effectMultiplier, myCard.buffDuration);
            else
                enemy.ApplyBuffOrDebuff((BuffTargetStat)myCard.statType, myCard.effectMultiplier, myCard.buffDuration);
        }
        else
        {
            if (myCard.effectType == CardEffectType.BuffSelf)
                enemy.ApplyBuffOrDebuff((BuffTargetStat)myCard.statType, myCard.effectMultiplier, myCard.buffDuration);
            else
                player.ApplyBuffOrDebuff((BuffTargetStat)myCard.statType, myCard.effectMultiplier, myCard.buffDuration);
        }

        usedThisCombat = true;
        canvasGroup.alpha = 0.5f; // Visually gray out used card
        canvasGroup.blocksRaycasts = false;
    }

    public void ResetForNewCombat()
    {
        usedThisCombat = false;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}