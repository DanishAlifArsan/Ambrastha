using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpiralBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private float angle = 0f;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire",0f,0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
       transform.Translate(moveDirection*speed*Time.deltaTime);
    }

    private void SetMoveDirection(Vector2 direction) {
        moveDirection = direction;
    }

    private void Fire() {
        float dirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float dirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(dirX, dirY, 0f);
        Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

        SetMoveDirection(bulletDirection);

        angle += 10;
    }
}
