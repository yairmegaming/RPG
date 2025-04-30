using UnityEngine;

public class EnemyScissors : EnemyDefault
{
    public override void EnemyChoosing()
    {
        int enemyChoice = Random.Range(1, 6);
        enemyManagerScript.EnemyChoice = enemyChoice switch
        {
            1 => EnemyChoiceEnum.Rock,
            2 => EnemyChoiceEnum.Paper,
            3 or 4 or 5 => EnemyChoiceEnum.Scissors,
            _ => EnemyChoiceEnum.none
        };
    }
}
