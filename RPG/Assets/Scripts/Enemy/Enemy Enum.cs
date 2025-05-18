using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState 
{
    none,
    EnemyChoosing,
    EnemyAttack,
    EnemyDefend,
    EnemyDamaged,
    EnemyDied,
}

public enum EnemyChoiceEnum
{
    none,
    Rock,
    Paper,
    Scissors,
}

public enum AttackTypeEnum
{
    none,
    damage,
    heal,
    buff,
    debuff,
}