using UnityEngine;

public class KlanaDeathState : IState
{
    private Animator animator;
    private ParticleSystem explosion;
    private GameObject magicCircle;

    public KlanaDeathState(Animator animator, ParticleSystem explosion, GameObject magicCircle)
    {
        this.animator = animator;
        this.explosion = explosion;
        this.magicCircle = magicCircle;
    }
    public void EnterState()
    {
        explosion.Play();
        magicCircle.SetActive(false);
        animator.SetTrigger("death");
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }
}
