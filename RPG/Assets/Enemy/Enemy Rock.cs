using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRock : EnemyDefault
{
    public override void EnemyAttack()
    {
        // Paper attack logic here
        var enemyChoice = Random.Range(1, 6); // Randomly choose between 1 and 3 for Rock, Paper, Scissors
        switch (enemyChoice)
        {
            case 1:
                enemyChoiceEnum = EnemyChoiceEnum.Rock;
                break;
            case 2:
                enemyChoiceEnum = EnemyChoiceEnum.Rock;
                break;
            case 3:
                enemyChoiceEnum = EnemyChoiceEnum.Rock;
                break;
            case 4:
                enemyChoiceEnum = EnemyChoiceEnum.Paper;
                break;
            case 5:
                enemyChoiceEnum = EnemyChoiceEnum.Scissors;
                break;
        }
    }

}
