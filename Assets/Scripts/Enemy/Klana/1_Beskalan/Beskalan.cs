using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beskalan : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject shockwave;
    [SerializeField] private GameObject bullet; 
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Text stageNumber, stageName;
    [SerializeField] private Transform playerTransform;
    Rigidbody2D enemyRB;

    private PlayerHealth health;

    StateMachine stateMachine;

    private void Awake() {
        health = GetComponent<PlayerHealth>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        stageName.text = "[BESKALAN]";
        stageNumber.text = "1/3";
        stateMachine = new StateMachine();

        var beskalanShockwave = new BeskalanShockwaveState(transform, shockwave, waypoints);
        var beskalanJump = new BeskalanJumpState(transform, waypoints, enemyRB, playerTransform, bullet);
        var klanaMoveToCenter = new KlanaMoveToCenterState(enemySpeed, transform, waypoints[2]);

        At(beskalanShockwave, beskalanJump, () => Vector2.Distance(transform.position, shockwave.transform.position) > 50f && stateMachine.CurrentState == beskalanShockwave);
        At(beskalanJump, beskalanShockwave, () => enemyRB.gravityScale == 0 && stateMachine.CurrentState == beskalanJump);

        stateMachine.SetState(beskalanShockwave);

        stateMachine.AddAnyTransition(klanaMoveToCenter, () => health.currentHealth <= 0);

        void At(IState from, IState to, Func<bool> condititon) {    // buat inisialisasi transisi
            stateMachine.AddTransition(from, to, condititon);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        stateMachine.UpdateState();
        CheckHealth();
    }

    private void CheckHealth() {
        if (health.currentHealth <= 0 && (Vector2.Distance(transform.position, new Vector2(transform.position.x, waypoints[2].transform.position.y)) < 1f)) {
            health.Respawn();
            GetComponent<Samandiman>().enabled = true;
            this.enabled = false;
        } 
    }
}
