using UnityEngine;

public class KlanaMoveToCenterState : IState
{
    private float enemySpeed;
    private Transform enemyTransform;
    private Transform waypoint;

    public KlanaMoveToCenterState(float enemySpeed, Transform enemyTransform, Transform waypoint)
    {
        this.enemySpeed = enemySpeed;
        this.enemyTransform = enemyTransform;
        this.waypoint = waypoint;
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
            enemyTransform.localScale = new Vector3(Mathf.Abs(enemyTransform.localScale.x) * -1, enemyTransform.localScale.y, enemyTransform.localScale.z);
        } else {
            enemyTransform.localScale = new Vector3(Mathf.Abs(enemyTransform.localScale.x), enemyTransform.localScale.y, enemyTransform.localScale.z);
        }
    }
}
