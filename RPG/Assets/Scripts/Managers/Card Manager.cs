using UnityEngine;

public class CardManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private EnemyManager enemyManager;

    private void Awake()
    {
        if (playerManager == null)
            playerManager = FindObjectOfType<PlayerManager>();
        if (enemyManager == null)
            enemyManager = FindObjectOfType<EnemyManager>();
        // Repeat for other managers as needed
    }

    public void PlayCard(Card card)
    {
        playerManager.UseCard(card);
        // Optionally trigger UI updates, animations, etc.
    }
}