using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] projectiles;
    [SerializeField] private float skillDuration;
    // Start is called before the first frame update
    private void Start()
    {
        foreach (var p in projectiles)
        {
            p.Stop();
        }
        StartCoroutine(moves1());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private IEnumerator moves1() {
        projectiles[0].Play();  
        yield return new WaitForSeconds(skillDuration);
        StartCoroutine(moves2());
        projectiles[0].Stop();
    }

    private IEnumerator moves2() {
        projectiles[1].Play();
        yield return new WaitForSeconds(skillDuration);
        StartCoroutine(moves1());
        projectiles[1].Stop();
    }
}
