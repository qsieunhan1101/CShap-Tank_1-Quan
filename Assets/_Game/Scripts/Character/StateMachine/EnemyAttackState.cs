using UnityEngine;

public class EnemyAttackState : IState<Enemy>
{
    float fireRate;
    float nextTimeToFire;
    public void OnEnter(Enemy character)
    {
        fireRate = 1.5f;
        nextTimeToFire = 1;
        character.Attack();
    }

    public void OnExecute(Enemy character)
    {
        if (character.EnemyTarget == null)
        {
            character.ChangeState(new EnemyIdleState());
        }
        if (character.IsTargetInAttackRange() == true)
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                character.Attack();

            }

        }
        else
        {
            character.ChangeState(new EnemyMoveToTargetState());
        }
    }

    public void OnExit(Enemy character)
    {
    }
}
