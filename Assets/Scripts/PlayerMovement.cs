using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    public Rigidbody2D body;
    // private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    // [SerializeField] private LayerMask wallLayer;

    // [SerializeField] private AudioClip jumpsound;
    
    // private float wallJumpCooldown;
    private float horizontalInput;

    // [SerializeField] private float coyoteTime;
    // private float coyoteCounter;

    // [SerializeField] private float wallJumpX;
    // [SerializeField] private float wallJumpY;
    

    [SerializeField] private float extraJumps;
    private float jumpCounter;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        }

        else if(horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1,1,1);
        }

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if(isGrounded()) {
            jumpCounter = extraJumps;
        }       

        // anim.SetBool("run",horizontalInput != 0);
        // anim.SetBool("ground", isGrounded());

        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        //adjustable jump height
        // if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0) {
        //     body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        // }

        // if(onWall()) {
        //     body.gravityScale = 0;
        //     body.velocity = Vector2.zero;
        // } else {
        //     body.gravityScale = 2.5f;
        //     body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //     if(isGrounded()) {
        //         coyoteCounter = coyoteTime;
        //         jumpCounter = extraJumps;
        //     } else {
        //         coyoteCounter -= Time.deltaTime;
        //     }
        // }
    }

    private void Jump() {
        // if(coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) {
        //     return;
        // }

        //  SoundManager.instance.playSound(jumpsound);

        // if (onWall()) {
        //     wallJump();
        //  } else {
        //     if (isGrounded()) {
        //         body.velocity = new Vector2(body.velocity.x,jumpPower);
        //     } else {
        //         if (coyoteCounter > 0) {
        //             body.velocity = new Vector2(body.velocity.x,jumpPower);
        //         }
        //         else {
        //             if (jumpCounter > 0) {
        //                 body.velocity = new Vector2(body.velocity.x,jumpPower);
        //                 jumpCounter--;
        //             }
        //         }
        //     }
        //     coyoteCounter = 0;
        //  }
         
        if(jumpCounter <= 0) {
            return;
        }

        
        if (isGrounded()) {
            body.velocity = new Vector2(body.velocity.x,jumpPower);
        } else {
            if (jumpCounter > 0) {
                    body.velocity = new Vector2(body.velocity.x,jumpPower);
                    jumpCounter--;
                }
        }
    }

    // private void wallJump() {
    //     body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    //     wallJumpCooldown = 0;
    // }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    
    }
    // private bool onWall() {
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
    //     return raycastHit.collider != null;
    // }

    public bool canAttack() {
        return horizontalInput == 0 && isGrounded();
    }
    
}
