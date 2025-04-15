using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Current Enemy")]
    public GameObject enemyPrefab;

    public EnemyEnum enemyEnum;
    public EnemyChoiceEnum enemyChoiceEnum;
    
    private EnemyDefault enemyDefault;

    private void Awake()
    {
        enemyDefault = enemyPrefab.GetComponent<EnemyDefault>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the enemy choice to none at the start
        enemyChoiceEnum = EnemyChoiceEnum.none;
        enemyEnum = EnemyEnum.EnemyChoice;

    }

    void Update()
    {
        if (enemyChoiceEnum == EnemyChoiceEnum.none)
        {
            enemyDefault.EnemyAttack();
            enemyEnum = EnemyEnum.EnemyChoice;
        }
    }

    public  EnemyChoiceEnum GetEnemyChoice()
    {
        return enemyChoiceEnum;
    }

    public void SetEnemyChoice(EnemyChoiceEnum choice)
    {
        enemyChoiceEnum = choice;
    }
}
