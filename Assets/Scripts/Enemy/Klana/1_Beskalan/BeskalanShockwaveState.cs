using UnityEngine;

public class BeskalanShockwaveState : IState
{
    private Transform enemyTransform;
    private GameObject shockwave;
    private Transform[] waypoints;
    private Vector2 direction;
    private SpriteRenderer enemySR;
    Transform from;

    public BeskalanShockwaveState(Transform enemyTransform, GameObject shockwave, Transform[] waypoints, Vector2 direction, SpriteRenderer enemySR)
    {
        this.enemyTransform = enemyTransform;
        this.shockwave = shockwave;
        this.waypoints = waypoints;
        this.direction = direction;
        this.enemySR = enemySR;
    }

    public void EnterState()
    {
        shockwave.transform.position = enemyTransform.position;
        shockwave.SetActive(true);
        if (Vector2.Distance(enemyTransform.position, waypoints[0].position) < 1f)
        {
            from = waypoints[0];
        } else {
            from = waypoints[1];
        }

        EnemyFacing();
    }

    public void ExitState()
    {
        shockwave.SetActive(false);
    }

    public void UpdateState()
    { 
        shockwave.transform.Translate(direction * 20 * Time.deltaTime); 
    }

    private void EnemyFacing() {
        if (enemyTransform.position.x < from.position.x)
        {
            enemySR.flipX= true;
            shockwave.transform.localScale = new Vector2(Mathf.Abs( shockwave.transform.localScale.x) * -1, shockwave.transform.localScale.y);
        } else {
            enemySR.flipX= false;
            shockwave.transform.localScale = new Vector2(Mathf.Abs( shockwave.transform.localScale.x), shockwave.transform.localScale.y);
        }
    }
}
