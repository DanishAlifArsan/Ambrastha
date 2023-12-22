using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gelu : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject[] objectProjectiles;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float skillDuration;
    [SerializeField] private Text stageNumber, stageName;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followRange;
    private Rigidbody2D enemyRB;
    private PlayerHealth health;
    private ParticleSystem[] particleSystems;

    StateMachine stateMachine;

    private void OnEnable() {
        enemyRB = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        particleSystems =  new ParticleSystem[objectProjectiles.Length];

        for (int i = 0; i < objectProjectiles.Length; i++)
        {
            objectProjectiles[i].SetActive(true);
             for (int j = 0; j < objectProjectiles[i].transform.childCount; j++)
            {
                particleSystems[i] = objectProjectiles[i].transform.GetChild(j).GetComponent<ParticleSystem>();
            }
        }
    }

    private void Start() {
        stageName.text = "[Gelu]";
        stageNumber.text = "2/2";

        stateMachine = new StateMachine();
        var pocongWalkToCenter = new PocongWalkState(enemySpeed, transform, waypoints[0]);
        var GeluChase = new GeluChaseState(transform, enemySpeed, playerTransform, followRange, skillDuration);
        var GeluShoot = new GeluShootState(transform, playerTransform, particleSystems, skillDuration);
        var pocongDeath = new PocongDeathState();

        At(pocongWalkToCenter, GeluChase, () => Vector2.Distance(transform.position, waypoints[0].transform.position) < .1f && stateMachine.CurrentState == pocongWalkToCenter);
        At(GeluChase, GeluShoot, () => enemyRB.bodyType == RigidbodyType2D.Static);
        At(GeluShoot, GeluChase, () => particleSystems[0].isStopped);

        stateMachine.AddAnyTransition(pocongDeath, () => health.currentHealth <= 0);

        stateMachine.SetState(pocongWalkToCenter);

        void At(IState from, IState to, Func<bool> condititon) {    // buat inisialisasi transisi
            stateMachine.AddTransition(from, to, condititon);
        }
    }

    private void Update() { 
        stateMachine.UpdateState();
        RotateBullet();
    }

    private void RotateBullet() {
        objectProjectiles[0].transform.RotateAround(transform.position, new Vector3(0,0,1), -90 * Time.deltaTime);
        objectProjectiles[1].transform.RotateAround(transform.position, new Vector3(0,0,1), -90 * Time.deltaTime);
        objectProjectiles[2].transform.RotateAround(transform.position, new Vector3(0,0,1), -90 * Time.deltaTime);
        objectProjectiles[3].transform.Rotate(new Vector3(0,0,1), -90 * Time.deltaTime);
    }
}
