using UnityEngine;

public abstract class EnemyDefault : MonoBehaviour
{
    [Header("Enemy Visuals")]
    public Sprite enemySprite;

    [Header("Enemy Manager")]
    public GameObject EnemyManager;
    protected EnemyManager enemyManagerScript;

    [Header("Enemy Combat")]
    public int EnemyDamage = 1;
    public int EnemyHealth = 3;
    public int EnemyDefense = 0;

    [Header("Enemy Score")]
    public int EnemyScoreWorth = 0;

    [Header("Enemy Animation")]
    public Animator enemyAnimator;
    public RuntimeAnimatorController enemyAnimatorController;

    public int EnemyCurrentDamage { get; set; }
    public int EnemyCurrentDefense { get; set; }
    public int EnemyCurrentHealth { get; set; }
    public int EnemyCurrentScoreWorth { get; set; }

    private void Awake()
    {
        enemyManagerScript = EnemyManager.GetComponent<EnemyManager>();
    }

    private void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        enemyManagerScript.EnemyChoice = EnemyChoiceEnum.none;
        EnemyCurrentHealth = EnemyHealth;
        EnemyCurrentDamage = EnemyDamage;
        EnemyCurrentDefense = EnemyDefense;
        EnemyCurrentScoreWorth = EnemyScoreWorth;
        enemyAnimator.runtimeAnimatorController = enemyAnimatorController;
    }

    public void TakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {EnemyCurrentHealth}");
    }

    public void Heal(int healAmount)
    {
        EnemyCurrentHealth = Mathf.Min(EnemyCurrentHealth + healAmount, EnemyHealth);
        Debug.Log($"Enemy healed {healAmount} health. Current health: {EnemyCurrentHealth}");
    }

    public void OnDefense(int damage, int armourPenetration = 0)
    {
        int effectiveDamage = Mathf.Max(1, damage - (EnemyCurrentDefense - armourPenetration));
        TakeDamage(effectiveDamage);
        Debug.Log($"Enemy defended with {EnemyCurrentDefense} defense. Damage taken: {effectiveDamage}");
    }

    public abstract void EnemyChoosing();

    public void EnemyAttackAnimation() => enemyAnimator.SetTrigger("Attack");
    public void EnemyDefendAnimation() => enemyAnimator.SetTrigger("Defend");
    public void EnemyDamagedAnimation() => enemyAnimator.SetTrigger("Damaged");
    public void EnemyDeathAnimation() => enemyAnimator.SetTrigger("Died");
}
