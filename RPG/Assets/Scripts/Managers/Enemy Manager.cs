using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemyPrefab;
    private EnemyState enemyState;
    private EnemyChoiceEnum enemyChoiceEnum;
    private EnemyDefault enemyDefault;

    private PlayerManager playerManager;
    private EnemyManager enemyManager;

    public EnemyState EnemyState
    {
        get => enemyState;
        set => enemyState = value;
    }
    public EnemyChoiceEnum EnemyChoice
    {
        get => enemyChoiceEnum;
        set => enemyChoiceEnum = value;
    }

    public int EnemyDamage
    {
        get => enemyDefault.EnemyDamage;
        set => enemyDefault.EnemyDamage = value;
    }

    public int EnemyHealth
    {
        get => enemyDefault.EnemyCurrentHealth;
        set => enemyDefault.EnemyCurrentHealth = value;
    }

    public int EnemyDefense
    {
        get => enemyDefault.EnemyCurrentDefense;
        set => enemyDefault.EnemyCurrentDefense = value;
    }

    public int EnemyScoreWorth
    {
        get => enemyDefault.EnemyCurrentScoreWorth;
        set => enemyDefault.EnemyCurrentScoreWorth = value;
    }

    public Sprite EnemySprite
    {
        get => enemyDefault.enemySprite;
        set => enemyDefault.enemySprite = value;
    }
    public GameObject EnemyPrefab
    {
        get => enemyPrefab;
        set => enemyPrefab = value;
    }

    private void Awake()
    {
        if (playerManager == null)
            playerManager = FindObjectOfType<PlayerManager>();
        if (enemyManager == null)
            enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void Start()
    {
        SpawnRandomEnemy();

        EnemyChoice = EnemyChoiceEnum.none;
        EnemyState = EnemyState.none;
    }

    private void Update()
    {
        if (enemyPrefab == null) return;

        enemyDefault = enemyPrefab.GetComponent<EnemyDefault>();

        switch (EnemyState)
        {
            case EnemyState.EnemyChoosing:
                enemyDefault.EnemyChoosing();
                break;
            case EnemyState.EnemyAttack:
                enemyDefault.EnemyAttackAnimation();
                break;
            case EnemyState.EnemyDefend:
                enemyDefault.EnemyDefendAnimation();
                break;
            case EnemyState.EnemyDamaged:
                enemyDefault.EnemyDamagedAnimation();
                break;
            case EnemyState.EnemyDied:
                enemyDefault.EnemyDeathAnimation();
                break;
        }
    }

    public void SpawnRandomEnemy()
    {
        // Destroy previous enemy if it exists
        if (enemyPrefab != null)
            Destroy(enemyPrefab);

        // Get a random enemy prefab from the database
        var allEnemies = GameDatabase.Instance.allEnemies;
        if (allEnemies == null || allEnemies.Count == 0)
        {
            Debug.LogWarning("No enemies found in GameDatabase!");
            return;
        }

        int randomIndex = Random.Range(0, allEnemies.Count);
        GameObject enemyToSpawn = allEnemies[randomIndex];

        // Instantiate the enemy prefab
        enemyPrefab = Instantiate(enemyToSpawn, transform.position, Quaternion.identity, transform);

        // Get the EnemyDefault component for stat access
        enemyDefault = enemyPrefab.GetComponent<EnemyDefault>();

        // Optionally, initialize or reset enemy stats here if needed
        // e.g., enemyDefault.ResetStats();

        // Set tag if needed for compatibility
        enemyPrefab.tag = "Enemy";
    }

    public void ResetEnemy()
    {
        if (enemyPrefab != null)
        {
            Destroy(enemyPrefab);
            enemyPrefab = null;
        }
        SpawnRandomEnemy();
    }
}

