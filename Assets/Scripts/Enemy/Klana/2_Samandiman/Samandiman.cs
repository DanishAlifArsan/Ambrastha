using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class Samandiman : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private float followRange;
    [SerializeField] private GameObject magicCircle;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform waypoint;
    [SerializeField] private Text stageNumber, stageName;
    PlayerHealth health;
    SpriteRenderer enemySR;
    StateMachine stateMachine;

    private void OnEnable() {
        health = GetComponent<PlayerHealth>();
        enemySR = GetComponent<SpriteRenderer>();
       transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // Start is called before the first frame update
    private void Start()
    {
        stageName.text = "[SAMANDIMAN]";
        stageNumber.text = "2/3";

        stateMachine = new StateMachine();

        var samandimanShoot = new SamandimanShootState(transform, enemySpeed, followRange, playerTransform, magicCircle, projectile, enemySR);
        var klanaSwitch = new KlanaSwitchState(enemySpeed, transform, waypoint, enemySR);

        stateMachine.SetState(samandimanShoot);

        stateMachine.AddAnyTransition(klanaSwitch, () => health.currentHealth <= 0);

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
        if (health.currentHealth <= 0 && (Vector2.Distance(transform.position, new Vector2(transform.position.x, waypoint.transform.position.y)) < 1f)) {
            health.Respawn();
            GetComponent<Bantarangin>().enabled = true;
            this.enabled = false;
        } 
    }
}
