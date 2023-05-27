using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBullet : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    // [Header ("Enemy")]
    // [SerializeField] private Transform enemy;

    // [Header ("Movement parameters")]
    [SerializeField] private float enemySpeed;
    // private Vector3 initScale;
    private bool movingLeft;

    [Header ("Idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    // [Header ("Enemy Animator")]
    // [SerializeField] private Animator anim;
    
    private void Awake()
    {
        // initScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if(movingLeft) {
            if (transform.position.x >= leftEdge.position.x)
            {
                moveInDirection(-1);
            } else {
                directionChange();
            }
            
        } else {
            if (transform.position.x <= rightEdge.position.x) {
                moveInDirection(1);
            } else {
                directionChange();
            }
        }
        
    }

    private void moveInDirection(int _direction) {
        idleTimer = 0;

        // anim.SetBool("moving", true);

        // enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        transform.position = new Vector3(transform.position.x + Time.deltaTime * enemySpeed * _direction,
        transform.position.y, transform.position.z);
    }

    private void directionChange() {
        // anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration) {
            movingLeft = !movingLeft;
        }
    }

    // private void OnDisable() {
    //     anim.SetBool("moving", false);
    // }
}
