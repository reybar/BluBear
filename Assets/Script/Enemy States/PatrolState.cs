using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState
{
    private enemy enemy;
    private float patrolTimer;
    private float patrolDuration;



    public void Execute()
    {
        if (enemy.melee)
        {
            Patrol();

            enemy.Move();
 
            if (enemy.Target != null && enemy.InMeleeRange )
            {
                enemy.ChangeState(new MeleeState());
            } 
        }

        else if (!enemy.melee)
        {
            if (enemy.Target != null && enemy.InThrowRange )
            {
                enemy.ChangeState(new RangedState());
            }
            enemy.ChangeState(new IdleState());
        }
        
    }

    public void Enter(enemy enemy)
    {
        patrolDuration = UnityEngine.Random.Range(5,10);
        this.enemy = enemy;
    }
    private void Patrol()
    {
        
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    
}
