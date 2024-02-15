using UnityEngine;

public class KlanaMoveToCenterState : IState
{
    private float enemySpeed;
    private Transform enemyTransform;
    private Transform waypoint;
    private SpriteRenderer enemySR;

    public KlanaMoveToCenterState(float enemySpeed, Transform enemyTransform, Transform waypoint, SpriteRenderer enemySR)
    {
        this.enemySpeed = enemySpeed;
        this.enemyTransform = enemyTransform;
        this.waypoint = waypoint;
        this.enemySR = enemySR;
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
         enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, new Vector2(enemyTransform.position.x, waypoint.position.y), Time.deltaTime * enemySpeed);
    }

    private void EnemyFacing() {
        if (enemyTransform.position.x > waypoint.position.x)
        {
            enemySR.flipX= true;
        } else {
            enemySR.flipX= true;
        }
    }
}
