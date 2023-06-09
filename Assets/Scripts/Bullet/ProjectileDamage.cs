using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    // ParticleSystem ps;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private float damage;

    // Start is called before the first frame update
    private void Awake()
    {
    //    ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        // List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            playerHealth.TakeDamage(damage);
            // ParticleSystem.Particle p = enter[i];
            // p.startColor = new Color32(255, 0, 0, 255);
            // enter[i] = p;
            

        }
        // for (int i = 0; i < numExit; i++)
        // {
        //     ParticleSystem.Particle p = exit[i];
        //     p.startColor = new Color32(0, 255, 0, 255);
        //     exit[i] = p;
        // }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}
