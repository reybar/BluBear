using UnityEngine;
using System.Collections;

public class RangedState : IEnemyState 
{
    private enemy enemy;

    private float throwTimer;
    private float throwCooldown = 2f;
    private bool canThrow = true;
    public void Execute()
    {
        ThrowNut();

        if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Enter(enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void ThrowNut()
    {
        throwTimer += Time.deltaTime;

        if (throwTimer >= throwCooldown)
        {
            canThrow = true;
            throwTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            enemy.MyAnimator.SetTrigger("throw");
        }
    }
}
