using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToTargetState : IState<Enemy>
{
    public void OnEnter(Enemy character)
    {
        character.Move(character.EnemyTarget.position);
    }

    public void OnExecute(Enemy character)
    {
        if (character.IsTargetInAttackRange() == true)
        {
            character.ChangeState(new EnemyAttackState());
        }
        if (character.EnemyTarget  == null)
        {
            character.ChangeState(new EnemyIdleState());
        }
    }

    public void OnExit(Enemy character)
    {
    }
}
