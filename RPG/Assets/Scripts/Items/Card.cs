using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardID;
    public string cardName;
    public Sprite cardImage;
    public string cardDescription;
    public int cardPower;
    public CardType cardType; // e.g., Attack, Heal, Buff, Debuff
}

public enum CardType
{
    Attack,
    Heal,
    Buff,
    Debuff
}