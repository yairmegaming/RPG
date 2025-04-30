using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Current Enemy")]
    public GameObject enemyPrefab;
    
    // Enemy state
    private EnemyState enemyState;
    private EnemyChoiceEnum enemyChoiceEnum;
    private EnemyDefault enemyDefault;

    public EnemyState EnemyState
    {
        get { return enemyState; }
        set { enemyState = value; }
    }
    public EnemyChoiceEnum EnemyChoice
    {
        get { return enemyChoiceEnum; }
        set { enemyChoiceEnum = value; }
    }

    public int EnemyDamage
    {
        get { return enemyDefault.EnemyDamage; }
        set {  enemyDefault.EnemyDamage = value; }
    }
    public int EnemyHealth
    {
        get { return enemyDefault.EnemyCurrentHealth ; }
        set { EnemyHealth = value; }
    }
    public int EnemyDefense
    {
        get { return enemyDefault.EnemyCurrentDefense; }
        set { enemyDefault.EnemyCurrentDefense = value; }
    }
    public int GetEnemyScoreWorth
    {
        get { return enemyDefault.EnemyCurrentScoreWorth; }
        set { enemyDefault.EnemyCurrentScoreWorth = value; }
    }

    public GameObject SetEnemyPrefab
    {
        get { return enemyPrefab; }
        set { enemyPrefab = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the enemy choice to none at the start
        EnemyChoice = EnemyChoiceEnum.none;
        EnemyState = EnemyState.none;
    }

    void Update()
    {
        // Check if the enemy prefab is set
        switch (EnemyState)
        {
            case EnemyState.none:
                enemyDefault = enemyPrefab.GetComponent<EnemyDefault>();
                break;
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

