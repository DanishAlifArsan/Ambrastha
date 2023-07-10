using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocongLogic : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject[] objectProjectiles;
    [SerializeField] private ParticleSystem[] particleProjectiles;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float skillDuration;
    [SerializeField] private Text stageNumber, stageName;
    private bool isChange;

    private bool isEntrance, isBulletMoving, isEntrance2, isEntrance3, isBattlePhase, isSecondPhase;
    // Start is called before the first frame update
    void Start()
    {
        stageName.text = "[Manuk Culi]";
        stageNumber.text = "1/2";
        isChange = true;
        foreach (var p in objectProjectiles)
        {
            p.SetActive(false);
        }
        foreach (var p in particleProjectiles)
        {
            p.Stop();
        }
        StartCoroutine(Entrance());
    }

    // Update is called once per frame
    void Update()
    {
        StageChange(isChange);
        if (isEntrance)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[0].transform.Translate(Vector3.right * 10 * Time.deltaTime);    
            }
        }
       
        if (isEntrance2)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[1].transform.position, Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[0].transform.Translate(Vector3.right * 10 * Time.deltaTime);    
            }
        }
        if (isEntrance3)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[2].transform.position, Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[0].transform.Translate(Vector3.down * 5 * Time.deltaTime);    
            }
        }

        if (isBattlePhase) {        
            if (isBulletMoving) 
            {
                objectProjectiles[1].transform.Translate(Vector3.zero);
                objectProjectiles[2].transform.Translate(Vector3.zero);
            } else {
                objectProjectiles[1].transform.Translate(Vector3.left * 10 * Time.deltaTime);    
                objectProjectiles[2].transform.Translate(Vector3.right * 10 * Time.deltaTime);  
            }
        }

        if (isSecondPhase)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[2].transform.position, Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[3].transform.RotateAround(transform.position, new Vector3(0,0,1), -90 * Time.deltaTime);
                objectProjectiles[4].transform.RotateAround(transform.position, new Vector3(0,0,1), -90 * Time.deltaTime);
                objectProjectiles[5].transform.RotateAround(transform.position, new Vector3(0,0,1), -90 * Time.deltaTime);
                objectProjectiles[6].transform.Rotate(new Vector3(0,0,1), -90 * Time.deltaTime);
            }
        }
    }

    private IEnumerator Entrance() {
        yield return new WaitForSeconds(1);
            objectProjectiles[0].transform.position = transform.position;
            isEntrance = true;
            yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[0].transform.position) < .1f);
            objectProjectiles[0].SetActive(true);
            isBulletMoving = true;
            yield return new WaitUntil(() => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 20f);
            isEntrance = false;
            isBulletMoving = false;
            objectProjectiles[0].SetActive(false);
            StartCoroutine(Entrance2());
    }

    private IEnumerator Entrance2() {
        objectProjectiles[0].transform.position = transform.position;
        isEntrance2 = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[1].transform.position) < .1f);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        objectProjectiles[0].SetActive(true);
        isBulletMoving = true;  
        yield return new WaitUntil(() => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 20f);
        isEntrance2 = false;
        isBulletMoving = false;
        objectProjectiles[0].SetActive(false);
        StartCoroutine(Entrance3());
    }

    private IEnumerator Entrance3() {
        objectProjectiles[0].transform.position = transform.position;
        isEntrance3 = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) < .1f);
        objectProjectiles[0].SetActive(true);
        isBulletMoving = true;  
        yield return new WaitUntil(() => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 10f);
        isEntrance3 = false;
        isBulletMoving = false;
        objectProjectiles[0].SetActive(false);
        StartCoroutine(BattlePhase());
    }

    private IEnumerator BattlePhase() {
        Transform temp1 = particleProjectiles[1].transform;
        Transform temp2 = particleProjectiles[2].transform;
        objectProjectiles[1].SetActive(true);
        objectProjectiles[2].SetActive(true);
        objectProjectiles[1].GetComponent<Rigidbody2D>().gravityScale = 0;
        objectProjectiles[2].GetComponent<Rigidbody2D>().gravityScale = 0;
        objectProjectiles[1].transform.position = transform.position;
        objectProjectiles[2].transform.position = transform.position;
        particleProjectiles[1].transform.parent = objectProjectiles[1].transform;
        particleProjectiles[2].transform.parent = objectProjectiles[2].transform;
        isBattlePhase = true;
        yield return new WaitForSeconds(0.55f);
        isBulletMoving = true;
        yield return new WaitForSeconds(0.5f);
        particleProjectiles[1].transform.parent = temp1;
        particleProjectiles[2].transform.parent = temp2;
        particleProjectiles[1].Play();
        particleProjectiles[2].Play();
        yield return new WaitForSeconds(skillDuration);
        particleProjectiles[1].Stop();
        particleProjectiles[2].Stop();
        objectProjectiles[1].GetComponent<Rigidbody2D>().gravityScale = 1;
        objectProjectiles[2].GetComponent<Rigidbody2D>().gravityScale = 1;
        yield return new WaitForSeconds(1.5f);
        objectProjectiles[1].SetActive(false);
        objectProjectiles[2].SetActive(false);
        isBattlePhase = false;
        isBulletMoving = false;
        StartCoroutine(SpinningAttack());
    }

    private IEnumerator SpinningAttack(){
        particleProjectiles[0].Play();
        yield return new WaitForSeconds(skillDuration);
        particleProjectiles[0].Stop();
        StartCoroutine(Entrance());
    }

    private void StageChange(bool isChange) {
        if (isChange)
        {
            if (GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                this.isChange = false;
                GetComponent<PlayerHealth>().Respawn();
                foreach (GameObject g in objectProjectiles)
                {
                    g.SetActive(false);
                }
                foreach (ParticleSystem p in particleProjectiles)
                {
                    p.Stop();
                }
                isEntrance = false;
                isBulletMoving = false; 
                isEntrance2 = false; 
                isEntrance3 = false; 
                isBattlePhase = false;
                StopAllCoroutines();
                StartCoroutine(SecondPhase());
            }
        }
    }

    private IEnumerator SecondPhase() {
        stageNumber.text = "2/2";
        stageName.text = "[Gelu]";
        isSecondPhase = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) < .1f);
        objectProjectiles[3].SetActive(true);
        objectProjectiles[4].SetActive(true);
        objectProjectiles[5].SetActive(true);
        objectProjectiles[6].SetActive(true);
        isBulletMoving = true;
        StartCoroutine(StopLaser());
        // yield return new WaitForSeconds(skillDuration);
    }

    private IEnumerator LaserShoot() {
        for (int i = 0; i < objectProjectiles[3].transform.childCount; i++)
        {
            objectProjectiles[3].transform.GetChild(i).gameObject.SetActive(true);
            objectProjectiles[4].transform.GetChild(i).gameObject.SetActive(true);
            objectProjectiles[5].transform.GetChild(i).gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(skillDuration);
        StartCoroutine(StopLaser());
    }

    private IEnumerator StopLaser() {   
        for (int i = 0; i < objectProjectiles[3].transform.childCount; i++)
        {
            objectProjectiles[3].transform.GetChild(i).gameObject.SetActive(false);
            objectProjectiles[4].transform.GetChild(i).gameObject.SetActive(false);
            objectProjectiles[5].transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(LaserShoot());
    }
}
