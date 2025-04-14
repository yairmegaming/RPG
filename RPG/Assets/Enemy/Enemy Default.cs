using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    
    [Header("Enemy Visuals")]
    public Sprite enemySprite;

    [Header("Enemy Manager")]
    public GameObject EnemyManager;
    private EnemyManager enemyManagerScript;
    private EnemyChoiceEnum enemyChoiceEnum;

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

    [Header("Enemy Choice Chance")]
    public int EnemyChoiceChanceRock = 1;
    public int EnemyChoiceChancePaper = 2;
    public int EnemyChoiceChanceScissors = 3;

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

    public void Attack()
    {
        var enemyChoice = Random.Range(1, 4); // Randomly choose between 1 and 3 for Rock, Paper, Scissors
        switch (enemyChoice)
        {
            case 1:
                enemyManagerScript.enemyChoiceEnum = EnemyChoiceEnum.Rock;
                break;
            case 2:
                enemyManagerScript.enemyChoiceEnum = EnemyChoiceEnum.Paper;
                break;
            case 3:
                enemyManagerScript.enemyChoiceEnum = EnemyChoiceEnum.Scissors;
                break;
        }
    }
     

    public void TakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + EnemyCurrentHealth);
    }
    public void Heal(int healAmount)
    {
        EnemyCurrentHealth += healAmount;
        Debug.Log("Enemy healed " + healAmount + " health. Current health: " + EnemyCurrentHealth);
    }
    public void onDefense(int _damage)
    {
        _damage -= EnemyCurrentDefense;
        TakeDamage(_damage);
        Debug.Log("Enemy defended " + EnemyCurrentDefense + " damage.)"); 
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

}
