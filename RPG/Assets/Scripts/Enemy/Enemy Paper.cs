using UnityEngine;

public class EnemyPaper : EnemyDefault
{
    public override void EnemyChoosing()
    {
        int enemyChoice = Random.Range(1, 6);
        enemyManagerScript.EnemyChoice = enemyChoice switch
        {
            1 => EnemyChoiceEnum.Rock,
            2 or 3 or 4 => EnemyChoiceEnum.Paper,
            5 => EnemyChoiceEnum.Scissors,
            _ => EnemyChoiceEnum.none
        };
    }
}
