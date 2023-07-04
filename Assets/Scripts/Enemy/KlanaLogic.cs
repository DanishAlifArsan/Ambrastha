using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KlanaLogic : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject[] objectProjectiles;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float skillDuration;
    [SerializeField] private Text stageNumber, stageName;
    private bool isChange;
    private float phase;

    private Vector3[] currentPosition, destPosition;

    private bool isEntrance, isBulletMoving, isEntrance2, isEntrance3, isSecondPhase, isSamandiman1, isSamandiman2, isThirdPhase, isBatarangin1, isBatarangin2;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = new Vector3[4];
        destPosition = new Vector3[2];
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        stageName.text = "[BESKALAN]";
        stageNumber.text = "1/3";
        isChange = true;
        phase = 1;
        foreach (var p in objectProjectiles)
        {
            p.SetActive(false);
        }
        StartCoroutine(Entrance());
    }

    // Update is called once per frame
    void Update()
    {
        StageChange();
        if (isEntrance)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[0].transform.Translate(Vector3.right * 10 * Time.deltaTime);    
            }
        }
       
        if (isEntrance2)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[2].transform.position, Time.deltaTime * enemySpeed);
        }

        if (isEntrance3)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[1].transform.position, Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[0].transform.Translate(Vector3.right * 10 * Time.deltaTime);    
            }
        }

        if (isSecondPhase)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[0].transform.position.x, waypoints[3].transform.position.y), Time.deltaTime * enemySpeed);
        }

        if(isSamandiman1) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[1].transform.position.x, waypoints[3].transform.position.y), Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[2].transform.Rotate(0.0f, 0.0f, 45*Time.deltaTime, Space.World);
            }
        }

        if (isSamandiman2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(waypoints[0].transform.position.x, waypoints[3].transform.position.y), Time.deltaTime * enemySpeed);
            if (isBulletMoving)
            {
                objectProjectiles[2].transform.Rotate(0.0f, 0.0f, -45*Time.deltaTime, Space.World);
            }
        }

        if (isThirdPhase)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[2].transform.position, Time.deltaTime * enemySpeed);
        }

        if (isBatarangin1)
        {
            objectProjectiles[3].SetActive(true);
            objectProjectiles[3].transform.position = Vector3.MoveTowards(objectProjectiles[3].transform.position, destPosition[0], Time.deltaTime * 5);           
            if (objectProjectiles[4].activeInHierarchy)
            {
                objectProjectiles[4].transform.position = Vector3.MoveTowards(objectProjectiles[4].transform.position, destPosition[1], Time.deltaTime * 5);    
            }

            if (Vector2.Distance(objectProjectiles[3].transform.position,currentPosition[0]) > 2)
            {
                objectProjectiles[4].SetActive(true);
            }
            
            if (Vector2.Distance(objectProjectiles[3].transform.position,destPosition[0]) < .1f)
            {
                objectProjectiles[3].transform.position = currentPosition[0];
            }

            if (Vector2.Distance(objectProjectiles[4].transform.position,destPosition[1]) < .1f)
            {
                objectProjectiles[4].transform.position = currentPosition[1];
            }
        }

        if (isBatarangin2)
        {
            objectProjectiles[5].SetActive(true);
            objectProjectiles[6].SetActive(true);
            objectProjectiles[5].transform.position = Vector3.MoveTowards(objectProjectiles[5].transform.position, new Vector3(objectProjectiles[5].transform.position.x, waypoints[4].transform.position.y), Time.deltaTime * 8);
            objectProjectiles[6].transform.position = Vector3.MoveTowards(objectProjectiles[6].transform.position, new Vector3(objectProjectiles[6].transform.position.x, waypoints[4].transform.position.y), Time.deltaTime * 8);
            if (Vector2.Distance(objectProjectiles[5].transform.position,new Vector3(objectProjectiles[5].transform.position.x, waypoints[4].transform.position.y)) < .1f)
            {
                objectProjectiles[5].transform.position = currentPosition[2];
            }
            if (Vector2.Distance(objectProjectiles[6].transform.position,new Vector3(objectProjectiles[6].transform.position.x, waypoints[4].transform.position.y)) < .1f)
            {
                objectProjectiles[6].transform.position = currentPosition[3];
            }
        }
    }

    private IEnumerator Entrance() {
        objectProjectiles[0].transform.position = transform.position;
        isEntrance = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) > 5f);
        objectProjectiles[1].SetActive(false);
            yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[0].transform.position) < .1f);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            yield return new WaitForSeconds(1);
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
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) < 5f);
        objectProjectiles[1].SetActive(true);
        yield return new WaitForSeconds(1);
        isEntrance2 = false;
        StartCoroutine(Entrance3());
    }

    private IEnumerator Entrance3() {
        objectProjectiles[0].transform.position = transform.position;
        isEntrance3 = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) > 5f);
        objectProjectiles[1].SetActive(false);
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[1].transform.position) < .1f);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        yield return new WaitForSeconds(1);
        objectProjectiles[0].SetActive(true);
        isBulletMoving = true;  
        yield return new WaitUntil(() => Vector2.Distance(transform.position, objectProjectiles[0].transform.position) > 20f);
        isEntrance3 = false;
        isBulletMoving = false;
        objectProjectiles[0].SetActive(false);
        StartCoroutine(Entrance4());
    }

    private IEnumerator Entrance4() {
        objectProjectiles[0].transform.position = transform.position;
        isEntrance2 = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) < 5f);
        objectProjectiles[1].SetActive(true);
        yield return new WaitForSeconds(1);
        isEntrance2 = false;
        StartCoroutine(Entrance());
    }

    private void StageChange() {
        if (isChange)
        {
            if (GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                this.isChange = false;
                phase++;
                GetComponent<PlayerHealth>().Respawn();
                foreach (GameObject g in objectProjectiles)
                {
                    g.SetActive(false);
                }
                switch (phase)
                {
                    case 2:
                        isEntrance = false;
                        isBulletMoving = false; 
                        isEntrance2 = false; 
                        isEntrance3 = false; 
                        StopAllCoroutines();
                        StartCoroutine(SecondPhase());
                        break;
                    case 3:
                        isSamandiman1 = false;
                        isSamandiman2 = false;
                        isBulletMoving = false;
                        isSecondPhase = false;
                        StopAllCoroutines();
                        StartCoroutine(ThirdPhase());
                        break;
                }
                
            }
        }
    }

    private IEnumerator SecondPhase() {
        stageNumber.text = "2/3";
        stageName.text = "[SAMANDIMAN]";
        isSecondPhase = true;
        isChange = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, new Vector3(waypoints[0].transform.position.x, waypoints[3].transform.position.y)) < .1f);
        objectProjectiles[2].SetActive(true);
        isSecondPhase = false;
        isBulletMoving = true;
        StartCoroutine(Samandiman1());
    }
    
    private IEnumerator Samandiman1() {
        yield return new WaitForSeconds(.1f);
        isSamandiman1 = true;
        GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, new Vector3(waypoints[1].transform.position.x, waypoints[3].transform.position.y)) < .1f);
        isSamandiman1 = false;
        StartCoroutine(Samandiman2());
    }

    private IEnumerator Samandiman2() {
        yield return new WaitForSeconds(.1f);
        isSamandiman2 = true;
        GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, new Vector3(waypoints[0].transform.position.x, waypoints[3].transform.position.y)) < .1f);
        isSamandiman2 = false;
        StartCoroutine(Samandiman1());
    }

     private IEnumerator ThirdPhase() {
        stageNumber.text = "3/3";
        stageName.text = "[BANTARANGIN]";
        isThirdPhase = true;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, waypoints[2].transform.position) < .1f);
        isThirdPhase = false;
        StartCoroutine(Batarangin1());
    }

    private IEnumerator Batarangin1() {
        objectProjectiles[5].SetActive(false);
        objectProjectiles[6].SetActive(false);
        currentPosition[0] = objectProjectiles[3].transform.position;
        destPosition[0] = objectProjectiles[4].transform.position;
        currentPosition[1] = objectProjectiles[4].transform.position;
        destPosition[1] = objectProjectiles[3].transform.position;
        isBatarangin1 = true;
        yield return new WaitForSeconds(skillDuration);
        isBatarangin1 = false;
        objectProjectiles[3].transform.position = currentPosition[0];
        objectProjectiles[4].transform.position = currentPosition[1];
        StartCoroutine(Batarangin2());
    }

    private IEnumerator Batarangin2() {
        objectProjectiles[3].SetActive(false);
        objectProjectiles[4].SetActive(false);
        currentPosition[2] = objectProjectiles[5].transform.position;
        currentPosition[3] = objectProjectiles[6].transform.position;
        isBatarangin2 = true;
        yield return new WaitForSeconds(skillDuration);
        isBatarangin2 = false;
        objectProjectiles[5].transform.position =  currentPosition[2];
        objectProjectiles[6].transform.position =  currentPosition[3];
        StartCoroutine(Batarangin1());
    }

    private void OnDisable() {
        foreach (var p in objectProjectiles)
        {
            if (p.activeInHierarchy)
            {
               p.SetActive(false); 
            }
        }
    }
}
