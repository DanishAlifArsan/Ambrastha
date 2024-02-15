using System;
using UnityEngine;

public class BeskalanMoveState : IState
{
    private float enemySpeed;
    private Transform enemyTransform, waypoint;
    private SpriteRenderer enemySR;
    private Transform playerTransform;
    private  GameObject bullet;

    public BeskalanMoveState(float enemySpeed, Transform enemyTransform, Transform waypoint, SpriteRenderer enemySR, Transform playerTransform, GameObject bullet)
    {
        this.enemySpeed = enemySpeed;
        this.enemyTransform = enemyTransform;
        this.waypoint = waypoint;
        this.enemySR = enemySR;
        this.playerTransform = playerTransform;
        this.bullet = bullet;
    }

    public void EnterState()
    {
        EnemyFacing();   
    }

    public void ExitState()
    {
       bullet.SetActive(false);
    }

    public void UpdateState()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, waypoint.position, Time.deltaTime * enemySpeed);
        
        if (Vector2.Distance(enemyTransform.position, playerTransform.position) < 8f)
        {
            bullet.SetActive(true);
        } else {
            bullet.SetActive(false);
        }
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
