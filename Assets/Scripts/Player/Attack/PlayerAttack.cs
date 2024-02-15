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
    private PlayerMovement playerMovement;
    private bool canAttack;
    ParticleSystem projectileParticle;

    private void Awake()
    {
        canAttack = true;
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
        
        if(Input.GetButtonDown("Fire1") && !playerMovement.isDashing && canAttack) {    
            
            StartCoroutine(Attack());
        } 
        if (playerMovement.isDashing) {
            StopCoroutine(Attack());
        }
    }

    private IEnumerator Attack() {
        canAttack = false;
        projectileParticle.Play();
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(attackCooldown);
        anim.SetBool("attack", false);
        projectileParticle.Stop();    
        ResetAttackDirection();   
        canAttack = true;
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
