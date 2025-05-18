using UnityEngine;

public class CardManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public EnemyManager enemyManager;

    public void PlayCard(Card card)
    {
        playerManager.UseCard(card);
        // Optionally trigger UI updates, animations, etc.
    }
}