using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    // [SerializeField] private Image totalHealthBar; 
    [SerializeField] private Image currentHealthBar; 
    public float currentHealth {get; private set;}
    // private Animator anim;
    [HideInInspector] public bool dead;
    // [HideInInspector] public float damageMultiplier = 10;

    [Header ("IFrame")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlash;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    // [Header ("Death sound")]
    // [SerializeField] private AudioClip deathSound;
    // [SerializeField] private AudioClip hurtSound;
    
    void Start()
    {
        currentHealth = startingHealth;
        // anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        // totalHealthBar.fillAmount = currentHealth / startingHealth;
    }

    public void TakeDamage(float _damage) {
        // _damage /= damageMultiplier;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0) {
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

        if (dead)
        {
            Deactivate();
        }
    }

    public void addHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
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
        gameObject.SetActive(false); 
    }

    public void Respawn() {
        dead = false;

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