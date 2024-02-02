using UnityEditor;
using UnityEngine;

public class BeskalanJumpState : IState
{
    private Transform enemyTransform;
    private Transform[] waypoints;
    private Rigidbody2D enemyRB;
    private Transform playerTransform;
    private GameObject bullet;
    Transform to, from;

    public BeskalanJumpState(Transform enemyTransform, Transform[] waypoints, Rigidbody2D enemyRB, Transform playerTransform, GameObject bullet)
    {
        this.enemyTransform = enemyTransform;
        this.waypoints = waypoints;
        this.enemyRB = enemyRB;
        this.playerTransform = playerTransform;
        this.bullet = bullet;
    }

    public void EnterState()
    {
        enemyRB.gravityScale = 1;
        if (Vector2.Distance(enemyTransform.position, waypoints[0].position) < 1f)
        {
            from = waypoints[0];
            to = waypoints[1];
        } else {
            from = waypoints[1];
            to = waypoints[0];
        }

        EnemyFacing();

        float v_x = (to.position.x-from.position.x)/2f;
        float v_y = (to.position.y-from.position.y)/2f + 4.9f * 2f;

        enemyRB.velocity = new Vector2(v_x, v_y);
    }

    public void ExitState()
    {
        bullet.SetActive(false);
        enemyRB.velocity = Vector2.zero;
        enemyRB.gravityScale = 0;
    }

    public void UpdateState()
    {
        if (Vector2.Distance(enemyTransform.position, playerTransform.position) < 8f)
        {
            bullet.SetActive(true);
        } else {
            bullet.SetActive(false);
        }

        if (Vector2.Distance(enemyTransform.position, to.position) < 1f)
        {
            enemyRB.gravityScale = 0;
        }
    }

    private void EnemyFacing() {
        if (enemyTransform.position.x > from.position.x)
        {
            enemyTransform.localScale = new Vector3(Mathf.Abs(enemyTransform.localScale.x), enemyTransform.localScale.y, enemyTransform.localScale.z);
        } else {
            enemyTransform.localScale = new Vector3(Mathf.Abs(enemyTransform.localScale.x) * -1, enemyTransform.localScale.y, enemyTransform.localScale.z);
        }
    }
}
