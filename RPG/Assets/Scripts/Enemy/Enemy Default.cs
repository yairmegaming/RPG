using UnityEngine;

public class EnemyDefault : MonoBehaviour
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

    [Header("Enemy Animation (Optional)")]
    public Animator enemyAnimator;
    public RuntimeAnimatorController enemyAnimatorController;

    [Header("Enemy Choice Chances (Sum should be > 0)")]
    [Range(0, 100)] public int rockChance = 33;
    [Range(0, 100)] public int paperChance = 33;
    [Range(0, 100)] public int scissorsChance = 34;

    public int EnemyCurrentDamage { get; set; }
    public int EnemyCurrentDefense { get; set; }
    public int EnemyCurrentHealth { get; set; }
    public int EnemyCurrentScoreWorth { get; set; }

    private void Awake()
    {
        if (EnemyManager != null)
            enemyManagerScript = EnemyManager.GetComponent<EnemyManager>();
    }

    private void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        if (enemyManagerScript != null)
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.none;
        EnemyCurrentHealth = EnemyHealth;
        EnemyCurrentDamage = EnemyDamage;
        EnemyCurrentDefense = EnemyDefense;
        EnemyCurrentScoreWorth = EnemyScoreWorth;
        if (enemyAnimator != null && enemyAnimatorController != null)
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

    // Set the chance for each choice in the Inspector (rockChance, paperChance, scissorsChance)
    public virtual void EnemyChoosing()
    {
        int total = rockChance + paperChance + scissorsChance;
        if (total <= 0)
        {
            Debug.LogWarning("Enemy choice chances are all zero! Defaulting to Rock.");
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Rock;
            return;
        }

        int roll = Random.Range(1, total + 1);
        if (roll <= rockChance)
        {
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Rock;
        }
        else if (roll <= rockChance + paperChance)
        {
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Paper;
        }
        else
        {
            enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Scissors;
        }
    }

    // Animation methods are now safe to call even if no Animator is assigned
    public void EnemyAttackAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Attack");
    }
    public void EnemyDefendAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Defend");
    }
    public void EnemyDamagedAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Damaged");
    }
    public void EnemyDeathAnimation()
    {
        if (enemyAnimator != null)
            enemyAnimator.SetTrigger("Died");
    }
}
