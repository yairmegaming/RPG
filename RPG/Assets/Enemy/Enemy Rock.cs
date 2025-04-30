using UnityEngine;

public class EnemyRock : EnemyDefault
{
    public override void EnemyChoosing()
    {
        int enemyChoice = Random.Range(1, 6);
        enemyManagerScript.EnemyChoice = enemyChoice switch
        {
            1 or 2 or 3 => EnemyChoiceEnum.Rock,
            4 => EnemyChoiceEnum.Paper,
            5 => EnemyChoiceEnum.Scissors,
            _ => EnemyChoiceEnum.none
        };
    }
}
