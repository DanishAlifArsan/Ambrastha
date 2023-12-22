using UnityEngine;

public class GeluChaseState : IState
{
    private Transform transform;
    private float enemySpeed;
    private Transform player;
    private float followRange;
    private float followDuration;
    private Rigidbody2D enemyRB;
    private SpriteRenderer enemySR;    
    float timer;
    bool isChasing;

    public GeluChaseState(Transform transform, float enemySpeed, Transform player, float followRange, float followDuration)
    {
        this.transform = transform;
        this.enemySpeed = enemySpeed;
        this.player = player;
        this.followRange = followRange;
        this.followDuration = followDuration;

        enemyRB = transform.GetComponent<Rigidbody2D>();
        enemySR = transform.GetComponent<SpriteRenderer>();
    }

    public void EnterState()
    {
        timer = 0;
        isChasing = true;
        enemyRB.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        if (!isChasing)
        {
            return;
        }

        timer += Time.deltaTime;
        Vector2 followPosition = new Vector2(player.position.x, transform.position.y);

        if (Vector2.Distance(transform.position, followPosition) > followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, followPosition, enemySpeed * Time.deltaTime);
        }

        if (player.position.x > transform.position.x)
        {
            enemySR.flipX = false;
        } else if (player.position.x < transform.position.x)
        {
            enemySR.flipX = true;
        }
        
        if (timer > followDuration)
        {
            timer = 0;
            isChasing = false; 
            enemyRB.bodyType = RigidbodyType2D.Static;
        }
    }
}
