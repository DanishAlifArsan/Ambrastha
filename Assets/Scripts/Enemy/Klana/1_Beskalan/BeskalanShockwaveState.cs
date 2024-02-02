using UnityEngine;

public class BeskalanShockwaveState : IState
{
    private Transform enemyTransform;
    private GameObject shockwave;
    private Transform[] waypoints;
    Transform from;

    public BeskalanShockwaveState(Transform enemyTransform, GameObject shockwave, Transform[] waypoints)
    {
        this.enemyTransform = enemyTransform;
        this.shockwave = shockwave;
        this.waypoints = waypoints;
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
        shockwave.transform.Translate(Vector3.right * 20 * Time.deltaTime); 
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
