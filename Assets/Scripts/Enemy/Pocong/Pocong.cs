using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pocong : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject[] objectProjectiles;
    [SerializeField] private GameObject[] particleProjectiles;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float skillDuration;
    [SerializeField] private Text stageNumber, stageName;
    [SerializeField] private Transform playerTransform;
    private PlayerHealth health;
    private ParticleSystem[] particleSystems;
    private float skillCooldown = 0;

    private int currentPhase;
    private bool isChange;

    StateMachine stateMachine;

    private void Awake() {
        health = GetComponent<PlayerHealth>();
        particleSystems =  new ParticleSystem[particleProjectiles.Length];
        for (int i = 0; i < particleProjectiles.Length; i++)
        {
            particleSystems[i] = particleProjectiles[i].GetComponent<ParticleSystem>();
        }
    }

    private void Start() {
        stageName.text = "[Manuk Culi]";
        currentPhase = 1;
        stageNumber.text = currentPhase + "/2";
        isChange = false;

        stateMachine = new StateMachine();
        var pocongShootToLeft = new PocongShootState(transform, objectProjectiles[0], Vector3.zero, -1);
        var pocongWalkToLeft = new PocongWalkState(enemySpeed, transform, waypoints[1]);
        var pocongShootToRight = new PocongShootState(transform, objectProjectiles[0], Vector3.zero, 1);
        var pocongWalkToRight = new PocongWalkState(enemySpeed, transform, waypoints[0]);
        var pocongWalkToUp = new PocongWalkState(enemySpeed, transform, waypoints[2]);
        var pocongShootToDown = new PocongShootState(transform, objectProjectiles[0], new Vector3(0,0,90), 0, 1);
        var pocongShootParticle = new PocongShootState(transform, particleProjectiles[0], Vector3.zero, 0,0, playerTransform);
    
        var pocongChange = new PocongChangeState(stageName, currentPhase);
        var pocongDeath = new PocongDeathState();

        At(pocongShootToLeft, pocongWalkToLeft, () => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 20f && stateMachine.CurrentState == pocongShootToLeft);
        At(pocongWalkToLeft, pocongShootToRight, () => Vector2.Distance(transform.position, waypoints[1].transform.position) < .1f && stateMachine.CurrentState == pocongWalkToLeft);
        At(pocongShootToRight, pocongWalkToUp, () => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 20f && stateMachine.CurrentState == pocongShootToRight);
        At(pocongWalkToUp, pocongShootToDown, () => Vector2.Distance(transform.position, waypoints[2].transform.position) < .1f && stateMachine.CurrentState == pocongWalkToUp);
        At(pocongShootToDown, pocongShootParticle, () => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 10f && stateMachine.CurrentState == pocongShootToDown);

        At(pocongShootParticle, pocongWalkToRight, () => skillCooldown >= skillDuration && stateMachine.CurrentState == pocongShootParticle);
        At(pocongWalkToRight, pocongShootToLeft, () => Vector2.Distance(transform.position, waypoints[0].transform.position) < .1f && stateMachine.CurrentState == pocongWalkToRight);

        stateMachine.AddAnyTransition(pocongChange, () => isChange);
        stateMachine.AddAnyTransition(pocongDeath, () => health.currentHealth <= 0);

        stateMachine.SetState(pocongShootToLeft);

        void At(IState from, IState to, Func<bool> condititon) {    // buat inisialisasi transisi
            stateMachine.AddTransition(from, to, condititon);
        }
    }

    private void Update() {
        stateMachine.UpdateState();
        CountSkillCooldown();
    }

    private void CheckHealth() {
        if (health.currentHealth <= 0) {
            isChange = false;
        } 
    }

    private void CountSkillCooldown() {
        if (particleSystems[0].isPlaying)
        {
            skillCooldown += Time.deltaTime;
            Debug.Log(skillCooldown);
            if (skillCooldown >= skillDuration + 1)
            {
                skillCooldown = 0;
            }
        }
    }
}
