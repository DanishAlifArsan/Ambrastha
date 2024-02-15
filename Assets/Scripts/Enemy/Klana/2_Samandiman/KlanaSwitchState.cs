using UnityEngine;

public class KlanaSwitchState : IState
{
    private float enemySpeed;
    private Transform enemyTransform;
    private Transform waypoint;
    private SpriteRenderer enemySR;

    public KlanaSwitchState(float enemySpeed, Transform enemyTransform, Transform waypoint, SpriteRenderer enemySR)
    {
        this.enemySpeed = enemySpeed;
        this.enemyTransform = enemyTransform;
        this.waypoint = waypoint;
        this.enemySR = enemySR;
    }

    public void EnterState()
    {
        
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, new Vector2(enemyTransform.position.x, waypoint.position.y), Time.deltaTime * enemySpeed);
        EnemyFacing();
    }

    private void EnemyFacing() {
        if (enemyTransform.position.x > waypoint.position.x)
        {
            enemySR.flipX = true;   
        } else {
            enemySR.flipX = false;
        }
    }
}
