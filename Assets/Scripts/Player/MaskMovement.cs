using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float followRange;
    [SerializeField] private Transform player;
   
    // Update is called once per frame
    private void Update()
    {
        Vector2 followPosition = new Vector2(player.position.x, player.position.y + followRange);
        //membuat fairy bergerak mengikuti miner
        if (Vector2.Distance(transform.position, followPosition) > followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, followPosition, speed * Time.deltaTime);
        }

        //membuat fairy menghadap ke arah player
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if (player.position.x < transform.position.y)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        } 
    }
}
