using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform enemy;
    private Animator anim;
    private PlayerHealth playerHealth;
    private float cooldownTimer = Mathf.Infinity;
    private PlayerMovement playerMovement;
    ParticleSystem projectileParticle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();  
        playerHealth = GetComponent<PlayerHealth>();
        projectileParticle = projectile.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        projectileParticle = projectile.GetComponentInChildren<ParticleSystem>();
        AttackDirection();
        
        if(Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown && playerMovement.canAttack()) {    
            projectileParticle.Play();
            anim.SetBool("attack", true);
            cooldownTimer = 0;
        } else if (Input.GetButtonUp("Fire1") || !playerMovement.canAttack()) {
        anim.SetBool("attack", false);
           projectileParticle.Stop();    
           ResetAttackDirection();   
        }

        cooldownTimer += Time.deltaTime;
    }

    private void AttackDirection() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput > .1f)
        {
            projectileParticle.transform.eulerAngles = new Vector3(0,90,0);
        }
        else if (horizontalInput < -.1f)
        {
            projectileParticle.transform.eulerAngles = new Vector3(0,-90,0);
        } 
        
        else if (verticalInput > .1f)
        {
            projectileParticle.transform.eulerAngles = new Vector3(-90,0,0);
        }
        else if (verticalInput < -.1f)
        {
            projectileParticle.transform.eulerAngles = new Vector3(90,0,0);
        }
    }

    private void ResetAttackDirection() {
        if (transform.localScale.x > .1f)
        {
            projectileParticle.transform.eulerAngles = new Vector3(0,90,0);
        } else {
            projectileParticle.transform.eulerAngles = new Vector3(0,-90,0);
        }
    }

    private void OnDisable() {
        projectileParticle.Stop(); 
    }
}
