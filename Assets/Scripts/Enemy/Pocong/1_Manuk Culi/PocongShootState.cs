using System;
using UnityEngine;

public class PocongShootState : IState
{
    private Transform enemyTransform;
    private GameObject projectile;
    private float projectileDirection;
    private float skillDuration;
    private Vector3 projectileRotation;
    private float projectileGravityScale;
    private Transform playerTransform;
    ParticleSystem projectileParticle;
    Rigidbody2D projectileRB;
    SpriteRenderer enemySR;

    float timer;

    public PocongShootState(Transform enemytransform, GameObject projectile, Vector3 projectileRotation, float projectileDirection = 0, float skillDuration = 0, float projectileGravityScale = 0, Transform playerTransform = null)
    {
        this.enemyTransform = enemytransform;
        this.projectile = projectile;
        this.projectileDirection = projectileDirection;
        this.projectileRotation = projectileRotation;
        this.skillDuration = skillDuration;
        this.projectileGravityScale = projectileGravityScale;
        this.playerTransform = playerTransform;

        projectileParticle = projectile.GetComponent<ParticleSystem>();
        projectileRB = projectile.GetComponent<Rigidbody2D>();
        enemySR = enemyTransform.GetComponent<SpriteRenderer>();
    }

    private bool isParticle() {
        return projectileParticle != null;
    }

    public void EnterState()
    { 
        if (isParticle())
        {
            projectileParticle.Play();
            timer = 0;
        } else {
            projectile.transform.position = enemyTransform.position;
            projectile.SetActive(true);
        }
    }

    public void ExitState()
    {
        if (!isParticle())
        {
            projectile.SetActive(false);
        }
    }

    public void UpdateState()
    {
        if (isParticle())
        {
            EnemyFacing(playerTransform);
            timer += Time.deltaTime;

            if (timer > skillDuration)
            {
                timer = 0;
                projectileParticle.Stop();
                return;   
            }
            
        } else {
            projectile.transform.rotation = Quaternion.Euler(projectileRotation);
            projectileRB.gravityScale = projectileGravityScale;   
            projectile.transform.Translate(new Vector3(projectileDirection, 0, 0) * 10 * Time.deltaTime);  
            EnemyFacing(projectile.transform);
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
