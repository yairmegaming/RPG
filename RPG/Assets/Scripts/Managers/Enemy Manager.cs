using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Current Enemy")]
    public GameObject enemyPrefab;
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

    private void Awake()
    {
        if (playerManager == null)
            playerManager = FindObjectOfType<PlayerManager>();
        if (enemyManager == null)
            enemyManager = FindObjectOfType<EnemyManager>();
        // Repeat for other managers as needed
    }

    private void Start()
    {
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
}

