using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private Image currentHealthBar; 
    [SerializeField] private Image avatar;
    [SerializeField] private Sprite hurtAvatar;
    public float currentHealth {get; private set;}
    [HideInInspector] public bool dead;
    [HideInInspector] public bool damageReduction = false;
    public float respawnCounter = 0;
    
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage) {
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
        }
        else {
            if(!dead) {
                dead = true;
            } 
        }
    }

    private void Update() {
        currentHealthBar.fillAmount = currentHealth / startingHealth;

        if (dead && respawnCounter <= 0)
        {
            dead = false;
            if (transform.parent != null)
            {
                MonoBehaviour[] comps = transform.parent.GetComponents<MonoBehaviour>();
                foreach(MonoBehaviour c in comps)
                {
                    c.enabled = false;
                }
                transform.parent.GetComponent<Animator>().SetTrigger("death");
            }
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

    public void Deactivate() {
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false); 
        }
    }

    public void Respawn() {
        dead = false;
        respawnCounter--;

        addHealth(startingHealth);

        gameObject.SetActive(true);
    }
}