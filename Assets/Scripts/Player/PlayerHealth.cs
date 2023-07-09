using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    // [SerializeField] private Image totalHealthBar; 
    [SerializeField] private Image currentHealthBar; 
    [SerializeField] private Image avatar;
    [SerializeField] private Sprite hurtAvatar;
    public float currentHealth {get; private set;}
    // private Animator anim;
    [HideInInspector] public bool dead;
    [HideInInspector] public bool damageReduction = false;

    public float respawnCounter = 0;
    // [HideInInspector] public float damageMultiplier = 10;

    // [Header ("IFrame")]
    // [SerializeField] private float iFrameDuration;
    // [SerializeField] private int numberOfFlash;
    // private SpriteRenderer spriteRend;

    // [Header ("Components")]
    // [SerializeField] private Behaviour[] components;

    // [Header ("Death sound")]
    // [SerializeField] private AudioClip deathSound;
    // [SerializeField] private AudioClip hurtSound;
    
    void Start()
    {
        currentHealth = startingHealth;
        // anim = GetComponent<Animator>();
        // spriteRend = GetComponent<SpriteRenderer>();
        // totalHealthBar.fillAmount = currentHealth / startingHealth;
    }

    public void TakeDamage(float _damage) {
        // _damage /= damageMultiplier;
        if (damageReduction)
        {
            _damage = Mathf.Round(_damage/2);
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0) {
            if (avatar !=  null)
            {
                StartCoroutine(Hurt());
            }
            // anim.SetTrigger("hurt");
            // StartCoroutine(IFrames());
            // SoundManager.instance.playSound(hurtSound);
        }
        else {
            if(!dead) {
                // foreach (Behaviour comp in components)
                // {
                //     comp.enabled = false;
                // }

                // anim.SetBool("ground", true);
                // anim.SetTrigger("die");

                dead = true;
                // SoundManager.instance.playSound(deathSound);
            } 
        }
    }

    private void Update() {
        currentHealthBar.fillAmount = currentHealth / startingHealth;

        if (dead && respawnCounter <= 0)
        {
            Deactivate();
        }
    }

    public void addHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Hurt() {
        Sprite temp = avatar.sprite;
        avatar.sprite = hurtAvatar;
        yield return new WaitForSeconds(0.25f);
        avatar.sprite = temp;
    }
 
    // private IEnumerator IFrames() {
    //     Physics2D.IgnoreLayerCollision(10,11,true);
    //     for (int i = 0; i < numberOfFlash; i++)
    //     {
    //         spriteRend.color = new Color(1,0,0, 0.5f);
    //         yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
    //         spriteRend.color = Color.white;
    //         yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
    //     }
    //     Physics2D.IgnoreLayerCollision(10,11,false);
    // }

    public void Deactivate() {
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false); 
        } else {
            gameObject.SetActive(false); 
        }
    }

    public void Respawn() {
        dead = false;
        respawnCounter--;

        addHealth(startingHealth);

        gameObject.SetActive(true);
        // anim.ResetTrigger("die");
        // anim.Play("Idle");
        // StartCoroutine(IFrames());

        // foreach (Behaviour comp in components)
        // {
        //     comp.enabled = true;
        // }
    }
}