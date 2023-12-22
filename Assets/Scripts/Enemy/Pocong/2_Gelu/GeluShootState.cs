using UnityEngine;

public class GeluShootState : IState
{
    private Transform enemyTransform;
    private Transform playerTransform;
    private ParticleSystem[] particleSystems;
    private float skillDuration;

    private SpriteRenderer enemySR;

    float timer;

    public GeluShootState(Transform enemyTransform, Transform playerTransform, ParticleSystem[] particleSystems, float skillDuration)
    {
        this.enemyTransform = enemyTransform;
        this.playerTransform = playerTransform;
        this.particleSystems = particleSystems;
        this.skillDuration = skillDuration;

        enemySR = enemyTransform.GetComponent<SpriteRenderer>();
    }

    public void EnterState()
    {
        timer = 0;
        foreach (var item in particleSystems)
        {
            item?.Play();
        }
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        timer += Time.deltaTime;
        EnemyFacing(playerTransform);

        if (timer > skillDuration)
        {
            timer = 0;
            foreach (var item in particleSystems)
            {
                item?.Stop();
            }  
        }
    }

    private void EnemyFacing(Transform target) {
        if (enemyTransform.position.x > target.position.x)
        {
            enemySR.flipX= true;
        } else {
            enemySR.flipX= false;
        }
    }
}
