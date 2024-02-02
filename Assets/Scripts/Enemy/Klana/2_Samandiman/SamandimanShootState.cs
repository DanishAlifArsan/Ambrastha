using UnityEngine;

public class SamandimanShootState : IState
{
    private Transform transform;
    private float enemySpeed;
    private float followRange;
    private Transform playerTransform;
    private GameObject magicCircle;
    private GameObject projectile;
    private  SpriteRenderer enemySR;

    public SamandimanShootState(Transform transform, float enemySpeed, float followRange, Transform playerTransform, GameObject magicCircle, GameObject projectile, SpriteRenderer enemySR)
    {
        this.transform = transform;
        this.enemySpeed = enemySpeed;
        this.followRange = followRange;
        this.playerTransform = playerTransform;
        this.magicCircle = magicCircle;
        this.projectile = projectile;
        this.enemySR = enemySR;
    }

    public void EnterState()
    {
        magicCircle.SetActive(true);
        projectile.SetActive(true);
    }

    public void ExitState()
    {
        magicCircle.SetActive(false);
        projectile.SetActive(false);
    }

    public void UpdateState()
    {
        magicCircle.transform.Rotate(new Vector3(0,0,1), -90 * Time.deltaTime);
        projectile.transform.Rotate(0.0f, 0.0f, 45*Time.deltaTime, Space.World);

        Vector2 followPosition = new Vector2(playerTransform.position.x, transform.position.y);

        if (Vector2.Distance(transform.position, followPosition) > followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, followPosition, enemySpeed * Time.deltaTime);
        }

        if (playerTransform.position.x > transform.position.x)
        {
            enemySR.flipX = false;
        } else if (playerTransform.position.x < transform.position.x)
        {
            enemySR.flipX = true;
        }
    }
}
