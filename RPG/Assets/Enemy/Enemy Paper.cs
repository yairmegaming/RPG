using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaper : EnemyDefault
{
    public override void EnemyChoosing()
    {
        // Paper attack logic here
        var enemyChoice = Random.Range(1, 6); // Randomly choose between 1 and 3 for Rock, Paper, Scissors
        switch (enemyChoice)
        {
            case 1:
                enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Rock;
                break;
            case 2:
                enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Paper;
                break;
            case 3:
                enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Paper;
                break;
            case 4:
                enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Paper;
                break;
            case 5:
                enemyManagerScript.EnemyChoice = EnemyChoiceEnum.Scissors;
                break;
        }
    } 
}
