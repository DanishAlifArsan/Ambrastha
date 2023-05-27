using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    // [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    // [SerializeField] private AudioClip fireballSound;

    [SerializeField] private Transform enemy;

    private PlayerHealth playerHealth;

    private float cooldownTimer = Mathf.Infinity;

    // private Animator anim;
    private PlayerMovement playerMovement;
    Transform projectileTransform;

    void Awake()
    {
        // anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();   
        projectile.SetActive(false);
        projectileTransform = projectile.GetComponent<Transform>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileTransform.LookAt(enemy);
        if(Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) {    
            projectile.SetActive(true);
            Attack();
        } else if (Input.GetMouseButtonUp(0)) {
            projectile.SetActive(false);
            playerMovement.body.bodyType = RigidbodyType2D.Dynamic;
        }

        cooldownTimer += Time.deltaTime;

        projectileTransform.position = new Vector3(this.transform.position.x, this.transform.position.y, projectileTransform.position.z);
    }

    private void Attack() {
        // SoundManager.instance.playSound(fireballSound);
        // cooldownTimer = 0;
        // anim.SetTrigger("attack");

        // fireballs[findFireball()].transform.position = firePoint.position;
        // fireballs[findFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        
        cooldownTimer = 0;
        playerMovement.body.bodyType = RigidbodyType2D.Static;
    }

    // private int findFireball() {
    //     for (int i = 0; i < fireballs.Length; i++)
    //     {
    //         if(!fireballs[i].activeInHierarchy) {
    //             return i;
    //         }
    //     }
        
    //     return 0;
    // }
}
