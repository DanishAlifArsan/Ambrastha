using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocongShootTwoBullets : IState
{
    private Transform enemyTransform;
    private GameObject leftProjectile, rightProjectile;
    private ParticleSystem leftParticle, rightParticle;
    private float skillDuration;
    private Transform playerTransform;

    private SpriteRenderer enemySR;
    MonoBehaviour mono;
    Transform temp1, temp2;
    bool isBulletMoving = false;

    public PocongShootTwoBullets(Transform enemyTransform, GameObject leftProjectile, GameObject rightProjectile, ParticleSystem leftParticle, ParticleSystem rightParticle, float skillDuration, Transform playerTransform, MonoBehaviour mono)
    {
        this.enemyTransform = enemyTransform;
        this.leftProjectile = leftProjectile;
        this.rightProjectile = rightProjectile;
        this.leftParticle = leftParticle;
        this.rightParticle = rightParticle;
        this.skillDuration = skillDuration;
        this.playerTransform = playerTransform;
        this.mono = mono;

        enemySR = enemyTransform.GetComponent<SpriteRenderer>();
    }

    public void EnterState()
    {
        temp1 = leftParticle.transform;
        temp2 = rightParticle.transform;
        leftProjectile.SetActive(true);
        rightProjectile.SetActive(true);
        leftProjectile.GetComponent<Rigidbody2D>().gravityScale = 0;
        rightProjectile.GetComponent<Rigidbody2D>().gravityScale = 0;
        leftProjectile.transform.position = enemyTransform.position;
        rightProjectile.transform.position = enemyTransform.position;
        leftParticle.transform.parent = leftProjectile.transform;
        rightParticle.transform.parent = rightProjectile.transform;
        
        isBulletMoving = true;
        
        mono.StartCoroutine(ShootTwoBullets());
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        EnemyFacing(playerTransform);

        if (isBulletMoving)
        {
            leftProjectile.transform.Translate(Vector3.left * 10 * Time.deltaTime);    
            rightProjectile.transform.Translate(Vector3.right * 10 * Time.deltaTime);
        } else {
            leftProjectile.transform.Translate(Vector3.zero);
            rightProjectile.transform.Translate(Vector3.zero);
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

    private IEnumerator ShootTwoBullets()
    {
        yield return new WaitForSeconds(0.55f);
        isBulletMoving = false;
        yield return new WaitForSeconds(0.5f);
        leftParticle.transform.parent = temp1;
        rightParticle.transform.parent = temp2;
        leftParticle.Play();
        rightParticle.Play();
        yield return new WaitForSeconds(skillDuration);
        leftParticle.Stop();
        rightParticle.Stop();
        leftProjectile.GetComponent<Rigidbody2D>().gravityScale = 1;
        rightProjectile.GetComponent<Rigidbody2D>().gravityScale = 1;
        yield return new WaitForSeconds(1.5f);
        leftProjectile.SetActive(false);
        rightProjectile.SetActive(false);
    }
}
