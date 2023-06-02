using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private float angle = 0f;
    private Vector2 moveDirection;
    private ParticleSystem ps;
     ParticleSystem.Particle[] particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        InitializeIfNeeded();
        // Extract copy
       
    //  // Do changes
    //  for (int i = 0; i < particles.Length; i++)
    //  {
    //      particles[i].position = Vector3.MoveTowards(particleEmitter.particles[i].position, spot.transform.position, Time.deltaTime * speed);
    //  }
    //  // Reassign back to emitter
    //  particleEmitter.particles = particles;

        int numParticlesAlive = ps.GetParticles(particles);
        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            // particles[i].velocity += Vector3.up * 10;
            Fire(particles[i]);
            particles[i].velocity = moveDirection*speed*Time.deltaTime;
        }

        // Apply the particle changes to the Particle System
        ps.SetParticles(particles, numParticlesAlive);
    }

    private void SetMoveDirection(Vector2 direction) {
        moveDirection = direction;
    }

    private void Fire(ParticleSystem.Particle particle) {
        float dirX = particle.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float dirY = particle.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(dirX, dirY, 0f);
        Vector2 bulletDirection = (bulletMoveVector - particle.position).normalized;

        SetMoveDirection(bulletDirection);

        angle += 10;
    }

    void InitializeIfNeeded()
    {
        if (ps == null)
            ps = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < ps.main.maxParticles)
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }
}
