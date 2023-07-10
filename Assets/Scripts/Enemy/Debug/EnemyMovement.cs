using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed;

    private int currentWaypointIndex = 0;
    
    // Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
  
    }

    // Update is called once per frame
    private void Update()
    {   
        // //menjalankan animasi gerak enemy berdasarkan arah gerak saat ini
        // anim.SetFloat("walkX", curVelocity.x);
        // anim.SetFloat("walkY", curVelocity.y);

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f){
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        } 
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}