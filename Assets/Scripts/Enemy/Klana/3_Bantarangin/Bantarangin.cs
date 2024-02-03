using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bantarangin : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private float followRange;
    [SerializeField] private GameObject magicCircle;
    [SerializeField] private GameObject[] projectileVertical, projectileHorizontal;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float skillDuration;
    [SerializeField] private Text stageNumber, stageName;
    PlayerHealth health;
    SpriteRenderer enemySR;
    StateMachine stateMachine;
    Animator anim;

    private void OnEnable() {
        health = GetComponent<PlayerHealth>();
        enemySR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        magicCircle.SetActive(true);
    }

    // Start is called before the first frame update
    private void Start()
    {
        stageName.text = "[BANTARANGIN]";
        stageNumber.text = "3/3";

        stateMachine = new StateMachine();

        var bantaranginLaserVertical = new BantaranginLaserState(projectileVertical, Vector2.down, skillDuration, enemySpeed);
        var bantaranginLaserHorizontal = new BantaranginLaserState(projectileHorizontal, Vector2.left, skillDuration, enemySpeed);
        var klanaDeath = new KlanaDeathState(anim, explosion, magicCircle);

        At(bantaranginLaserVertical, bantaranginLaserHorizontal, () => !projectileVertical[0].activeInHierarchy && stateMachine.CurrentState == bantaranginLaserVertical);
        At(bantaranginLaserHorizontal, bantaranginLaserVertical, () => !projectileHorizontal[0].activeInHierarchy && stateMachine.CurrentState == bantaranginLaserHorizontal);

        stateMachine.SetState(bantaranginLaserVertical);

        stateMachine.AddAnyTransition(klanaDeath, () => health.currentHealth <= 0);

        void At(IState from, IState to, Func<bool> condititon) {    // buat inisialisasi transisi
            stateMachine.AddTransition(from, to, condititon);
        }
    }

    // Update is called once per frame
     private void Update()
    {
        stateMachine.UpdateState();
        RotateMagicCircle();
        FollowPlayer();
        EnemyFacing();
    }

    private void RotateMagicCircle() {
        magicCircle.transform.Rotate(new Vector3(0,0,1), -90 * Time.deltaTime);
    }

    private void FollowPlayer() {
        Vector2 followPosition = new Vector2(playerTransform.position.x, transform.position.y);

        if (Vector2.Distance(transform.position, followPosition) > followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, followPosition, enemySpeed * Time.deltaTime);
        }
    }

    private void EnemyFacing() {
        if (playerTransform.position.x > transform.position.x)
        {
            enemySR.flipX = false;
        } else if (playerTransform.position.x < transform.position.x)
        {
            enemySR.flipX = true;
        }
    }
}
