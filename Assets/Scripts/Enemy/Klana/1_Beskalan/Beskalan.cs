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
    SpriteRenderer enemySR;

    private PlayerHealth health;

    StateMachine stateMachine;

    private void Awake() {
        health = GetComponent<PlayerHealth>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemySR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        stageName.text = "[BESKALAN]";
        stageNumber.text = "1/3";
        stateMachine = new StateMachine();

        var beskalanShockwaveRight = new BeskalanShockwaveState(transform, shockwave, waypoints, Vector2.right, enemySR);
        var beskalanShockwaveLeft = new BeskalanShockwaveState(transform, shockwave, waypoints, Vector2.left, enemySR);
        var klanaMoveToRight = new BeskalanMoveState(enemySpeed, transform, waypoints[0], enemySR, playerTransform, bullet); 
        var klanaMoveToLeft = new BeskalanMoveState(enemySpeed, transform, waypoints[1], enemySR, playerTransform, bullet); 
        var klanaMoveToCenter = new BeskalanMoveState(enemySpeed, transform, waypoints[2], enemySR, playerTransform, bullet); 
        var klanaMoveToCenter2 = new BeskalanMoveState(enemySpeed, transform, waypoints[2], enemySR, playerTransform, bullet); 
        var klanaMoveToSwitch = new KlanaMoveToCenterState(enemySpeed, transform, waypoints[2], enemySR); 

        At(beskalanShockwaveLeft, klanaMoveToCenter, () => Vector2.Distance(transform.position, shockwave.transform.position) > 50f && stateMachine.CurrentState == beskalanShockwaveLeft);
        At(klanaMoveToCenter, klanaMoveToLeft, () => Vector2.Distance(transform.position, waypoints[2].position) < .1f && stateMachine.CurrentState == klanaMoveToCenter);
        At(klanaMoveToLeft, beskalanShockwaveRight, () => Vector2.Distance(transform.position, waypoints[1].position) < .1f && stateMachine.CurrentState == klanaMoveToLeft);
        At(beskalanShockwaveRight, klanaMoveToCenter2, () => Vector2.Distance(transform.position, shockwave.transform.position) > 50f && stateMachine.CurrentState == beskalanShockwaveRight);
        At(klanaMoveToCenter2, klanaMoveToRight, () => Vector2.Distance(transform.position, waypoints[2].position) < .1f && stateMachine.CurrentState == klanaMoveToCenter2);
        At(klanaMoveToRight, beskalanShockwaveLeft, () => Vector2.Distance(transform.position, waypoints[0].position) < .1f && stateMachine.CurrentState == klanaMoveToRight);

        stateMachine.SetState(beskalanShockwaveLeft);

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
