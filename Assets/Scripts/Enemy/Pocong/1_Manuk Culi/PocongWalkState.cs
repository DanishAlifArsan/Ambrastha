using System;
using UnityEngine;

public class PocongWalkState : IState
{
    private float enemySpeed;
    private Transform enemyTransform, waypoint;
    private SpriteRenderer enemySR;

    public PocongWalkState(float enemySpeed, Transform enemyTransform, Transform waypoint)
    {
        this.enemySpeed = enemySpeed;
        this.enemyTransform = enemyTransform;
        this.waypoint = waypoint;

        enemySR = enemyTransform.GetComponent<SpriteRenderer>();
    }

    public void EnterState()
    {
        EnemyFacing();   
    }

    public void ExitState()
    {
       
    }

    public void UpdateState()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, waypoint.position, Time.deltaTime * enemySpeed);
    }

    private void EnemyFacing() {
        if (enemyTransform.position.x > waypoint.position.x)
        {
            enemySR.flipX= true;
        } else {
            enemySR.flipX= false;
        }
    }
}
