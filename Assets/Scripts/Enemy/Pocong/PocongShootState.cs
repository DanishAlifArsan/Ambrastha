using System;
using UnityEngine;

public class PocongShootState : IState
{
    private Transform enemyTransform;
    private GameObject projectile;
    private float projectileDirection;
    private Vector3 projectileRotation;
    private float projectileGravityScale;
    private Transform playerTransform;
    ParticleSystem projectileParticle;
    Rigidbody2D projectileRB;
    SpriteRenderer enemySR;

    public PocongShootState(Transform enemytransform, GameObject projectile, Vector3 projectileRotation, float projectileDirection = 0, float projectileGravityScale = 0, Transform playerTransform = null)
    {
        this.enemyTransform = enemytransform;
        this.projectile = projectile;
        this.projectileDirection = projectileDirection;
        this.projectileRotation = projectileRotation;
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
        } else {
            projectile.transform.position = enemyTransform.position;
            projectile.SetActive(true);
        }
    }

    public void ExitState()
    {
        if (isParticle())
        {
            projectileParticle.Stop();
        } else {
            projectile.SetActive(false);
        }
    }

    public void UpdateState()
    {
        if (isParticle())
        {
            EnemyFacing(playerTransform);
            return;
        }
        projectile.transform.rotation = Quaternion.Euler(projectileRotation);
        projectileRB.gravityScale = projectileGravityScale;   
        projectile.transform.Translate(new Vector3(projectileDirection, 0, 0) * 10 * Time.deltaTime);  
        EnemyFacing(projectile.transform);
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
