using UnityEngine;

public class PocongDeathState : IState
{
    private Animator animator;
    private ParticleSystem explosion;
    private GameObject[] objectProjectiles;
    private bool isAnimPlayed;

    public PocongDeathState(Animator animator, ParticleSystem explosion, GameObject[] objectProjectiles)
    {
        this.animator = animator;
        this.explosion = explosion;
        this.objectProjectiles = objectProjectiles;
    }

    public void EnterState()
    {
        explosion.Play();
        foreach (var item in objectProjectiles)
        {
            item.SetActive(false);
        }
        animator.SetTrigger("death");
        isAnimPlayed = true;
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        // if (isAnimPlayed)
        // {
        //     isAnimPlayed = false;
        //     animator.SetTrigger("death");
        // }
    }
}
