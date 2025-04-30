using System.Collections;
using System.Collections.Generic;
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

    // Animation Variables
    [Header("Enemy Animation")]
    public Animator enemyAnimator;
    public RuntimeAnimatorController enemyAnimatorController;
    
    public int EnemyCurrentDamage
    {
        get { return EnemyCurrentDamage; }
        set { EnemyCurrentDamage = value; }
    }
    public int EnemyCurrentDefense
    {
        get { return EnemyCurrentDefense; }
        set { EnemyCurrentDefense = value; }
    }
    
    public int EnemyCurrentHealth
    {
        get { return EnemyCurrentHealth; }
        set { EnemyCurrentHealth = value; }
    }
    public int EnemyCurrentScoreWorth
    {
        get { return EnemyCurrentScoreWorth; }
        set { EnemyCurrentScoreWorth = value; }
    }

    void Awake()
    {
        enemyManagerScript = EnemyManager.GetComponent<EnemyManager>();
    }

    void Start()
    {
        // Set the enemy choice to none at the start
        enemyManagerScript.EnemyChoice = EnemyChoiceEnum.none;

        // Set the enemy state to choosing
        EnemyCurrentHealth = EnemyHealth;
        EnemyCurrentDamage = EnemyDamage;
        EnemyCurrentDefense = EnemyDefense;
        EnemyCurrentScoreWorth = EnemyScoreWorth;

        // Set the enemy animator controller
        enemyAnimator.runtimeAnimatorController = enemyAnimatorController;
    }

    // Enemy Attack Method
    // This method is used to deal damage to the player. It takes the player's current health and subtracts the enemy's current damage from it.
    // It also checks if the player is defending and reduces the damage accordingly.
    public void TakeDamage(int _damage)
    {
        EnemyCurrentHealth -= _damage;
        Debug.Log("Enemy took " + _damage + " damage. Current health: " + EnemyCurrentHealth);
    }
    // Enemy Heal Method
    // This method is used to heal the enemy. It adds the heal amount to the current health.
    // If the current health exceeds the max health, it sets the current health to the max health.
    public void Heal(int _healAmount)
    {
        EnemyCurrentHealth += _healAmount;
        Debug.Log("Enemy healed " + _healAmount + " health. Current health: " + EnemyCurrentHealth);
    }

    // Enemy Defense Method
    // This method is used to reduce the damage taken by the enemy. It subtracts the enemy's current defense from the incoming damage.
    // If the damage is less than or equal to the defense, it sets the damage to 1.S
    public void onDefense(int _damage, int _armourPenetration = 0)
    {
        _damage -= EnemyCurrentDefense - _armourPenetration;
        if (_damage <= 0)
        {
            _damage = 1;
        }
        TakeDamage(_damage);
        Debug.Log("Enemy defended " + EnemyCurrentDefense + " damage.)"); 
    }

    // Enemy State Methods
    public abstract void EnemyChoosing();

    // Enemy Animation Methods
    public void EnemyAttackAnimation()
    {
        enemyAnimator.SetTrigger("Attack");
    }
    public void EnemyDefendAnimation()
    {
        enemyAnimator.SetTrigger("Defend");
    }
    public void EnemyDamagedAnimation()
    {
        enemyAnimator.SetTrigger("Damaged");
    }
    public void EnemyDeathAnimation()
    {
        enemyAnimator.SetTrigger("Died");
    }
}
