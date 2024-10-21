using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<Enemy>
{
    float time;
    float timeInState;
    public void OnEnter(Enemy character)
    {
        time = 0;
        timeInState = Random.Range(3,4);
        character.StopMove();
    }

    public void OnExecute(Enemy character)
    {
        time += Time.deltaTime;
        if (time >= timeInState)
        {
            character.ChangeState(new EnemyPatrolState());
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
