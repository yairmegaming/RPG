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
    protected EnemyChoiceEnum enemyChoiceEnum;

    [Header("Enemy Combat")]
    public int EnemyDamage = 1;
    public int EnemyHealth = 3;
    public int EnemyDefense = 0;

    private int EnemyCurrentHealth;
    private int EnemyCurrentDamage;
    private int EnemyCurrentDefense;

    [Header("Enemy Score")]
    public int EnemyScoreWorth = 0;
    private int EnemyCurrentScoreWorth;

    void Awake()
    {
        enemyManagerScript = EnemyManager.GetComponent<EnemyManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyCurrentDamage = EnemyDamage;
        EnemyCurrentHealth = EnemyHealth;
        EnemyCurrentDefense = EnemyDefense;

        enemyManagerScript.enemyChoiceEnum = EnemyChoiceEnum.none;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyCurrentHealth <= 0)
        {
            EnemyCurrentHealth = 0;
            Destroy(gameObject);
            Debug.Log("Enemy Died");
        }
    }

    // Enemy Attack Method
    // This method is used to deal damage to the player. It takes the player's current health and subtracts the enemy's current damage from it.
    // It also checks if the player is defending and reduces the damage accordingly.
    public void TakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + EnemyCurrentHealth);
    }
    // Enemy Heal Method
    // This method is used to heal the enemy. It adds the heal amount to the current health.
    // If the current health exceeds the max health, it sets the current health to the max health.
    public void Heal(int healAmount)
    {
        EnemyCurrentHealth += healAmount;
        Debug.Log("Enemy healed " + healAmount + " health. Current health: " + EnemyCurrentHealth);
    }

    // Enemy Defense Method
    // This method is used to reduce the damage taken by the enemy. It subtracts the enemy's current defense from the incoming damage.
    // If the damage is less than or equal to the defense, it sets the damage to 1.
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

    // Buff and Debuff Methods

    // These methods are used to increase the enemy's damage and defense. They take a percentage as input and increase the current damage and defense accordingly.
    // The percentage is calculated by multiplying the current damage/defense by the percentage and dividing by 100.
    // The new damage and defense values are then set to the current damage and defense.
    public void BuffEnemyDamage(int _buffPercentage)
    {
        EnemyCurrentDamage += (EnemyCurrentDamage * _buffPercentage) / 100;
        Debug.Log("Enemy buffed damage by " + _buffPercentage + "%. Current damage: " + EnemyCurrentDamage);

    }
    public void BuffEnemyDefense(int _buffPercentage)
    {
        EnemyCurrentDefense += (EnemyCurrentDefense * _buffPercentage) / 100;
        Debug.Log("Enemy buffed defense by " + _buffPercentage + "%. Current defense: " + EnemyCurrentDefense);
    }
    public void DebuffEnemyDamage(int _debuffPercentage)
    {
        EnemyCurrentDamage -= (EnemyCurrentDamage * _debuffPercentage) / 100;
        Debug.Log("Enemy debuffed damage by " + _debuffPercentage + "%. Current damage: " + EnemyCurrentDamage);
    }
    public void DebuffEnemyDefense(int _debuffPercentage)
    {
        EnemyCurrentDefense -= (EnemyCurrentDefense * _debuffPercentage) / 100;
        Debug.Log("Enemy debuffed defense by " + _debuffPercentage + "%. Current defense: " + EnemyCurrentDefense);
    }

    public int GetEnemyCurrentDamage()
    {
        return EnemyCurrentDamage;
    }
    public int GetEnemyCurrentDefense()
    {
        return EnemyCurrentDefense;
    }
    public int GetEnemyCurrentHealth()
    {
        return EnemyCurrentHealth;
    }
    public int GetEnemyScoreWorth()
    {
        return EnemyCurrentScoreWorth;
    }

    public abstract void EnemyAttack();
}
