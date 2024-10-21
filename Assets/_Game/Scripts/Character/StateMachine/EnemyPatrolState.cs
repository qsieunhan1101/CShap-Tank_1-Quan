using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState<Enemy>
{
    float time;
    float timeInState;
    int randomNextState;
    public void OnEnter(Enemy character)
    {
        time = 0;
        timeInState = Random.Range(7, 9);
        randomNextState = Random.Range(1,3);
        character.Move(character.RandomPositionInMap());
        
    }

    public void OnExecute(Enemy character)
    {
        time += Time.deltaTime;
        if (character.IsFinishMove() == true || time >= timeInState)
        {
            if (randomNextState == 1)
            {
                character.ChangeState(new EnemyIdleState());
            }
            if (randomNextState == 2)
            {
                character.ChangeState(new EnemyPatrolState());
            }
        }

        if (character.EnemyTarget != null)
        {
            character.ChangeState(new EnemyMoveToTargetState());
        }
    }

    public void OnExit(Enemy character)
    {
    }
}
