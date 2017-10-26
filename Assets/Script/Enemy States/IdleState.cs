using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState 
{

    private enemy enemy;

    private float idleTimer;

    
    private float idleDuration;

    public void Execute()
    {
        Idle();

        if (enemy.Target != null && enemy.melee)
        {
            enemy.ChangeState(new PatrolState());
        }
        else if (enemy.Target != null && !enemy.melee)
        {
            enemy.ChangeState(new RangedState());
        }
        
    }

    public void Enter(enemy enemy)
    {
        idleDuration = UnityEngine.Random.Range(2, 5);
        this.enemy = enemy;
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration && enemy.melee)
        {
            enemy.ChangeState(new PatrolState());
        }
        else if (idleTimer >= idleDuration && !enemy.melee)
        {
            enemy.ChangeDirection();
            enemy.ChangeState(new PatrolState());
        }
    }
}
