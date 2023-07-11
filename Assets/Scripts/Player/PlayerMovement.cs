using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashCost;
    [SerializeField] private float dashCooldown;
    [SerializeField] private TrailRenderer trailRenderer;
    public Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private float startingStamina;
    [SerializeField] private float staminaRegen;
    [SerializeField] private Image currentStaminaBar; 
    public float currentStamina {get; private set;}
    private float horizontalInput;
    [SerializeField] private float extraJumps;
    private float jumpCounter;
    public bool isDashing { get; private set; }
    private bool canDash;

    private void Awake()
    {
        isDashing = false;
        canDash = true;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentStamina = startingStamina;
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput > 0.01f) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if(horizontalInput < -0.01f) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if(isGrounded()) {
            jumpCounter = extraJumps;
        }       

        anim.SetBool("run",horizontalInput != 0);
        anim.SetBool("jump",!isGrounded());

        if(Input.GetButtonDown("Jump")) {
            Jump();
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (currentStamina - dashCost > 0 && canDash)
            {
                currentStamina -= dashCost;
                StartCoroutine(Dash());
            }
        }

        // stamina related 
        currentStaminaBar.fillAmount = currentStamina / startingStamina;
        if (currentStamina < startingStamina) 
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }
        else if (currentStamina > startingStamina) {
            currentStamina = startingStamina;
        }

        //adjustable jump height
        if(Input.GetButtonUp("Jump") && body.velocity.y > 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }
    }

    private void Jump() {    
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

    private IEnumerator Dash() {
        hitbox.enabled = false;
        canDash = false;
        isDashing = true;
        float playerGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(transform.localScale.x * dashPower, 0);
        trailRenderer.emitting = true;
        anim.SetBool("dashing",true);
        yield return new WaitForSeconds(dashTime);
        anim.SetBool("dashing",false);
        trailRenderer.emitting = false;
        hitbox.enabled = true;
        body.gravityScale = playerGravity;
        isDashing = false;
        canDash = true;
    }

    private bool isGrounded() {
        return Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    
}
