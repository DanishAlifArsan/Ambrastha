using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            if ( collision.GetComponent<PlayerHealth>() != null)
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(1);
            }
        }       
    }
}
