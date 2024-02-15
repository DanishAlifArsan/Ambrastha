using UnityEngine;

public class BantaranginLaserState : IState
{
    private GameObject[] projectile;
    private Vector2 direction;
    private float skillDuration;
    private float enemySpeed;
    private Vector3[] currentPosition, destPosition;
    float timer;

    public BantaranginLaserState(GameObject[] projectile,Vector2 direction, float skillDuration, float enemySpeed)
    {
        this.projectile = projectile;
        this.direction = direction;
        this.skillDuration = skillDuration;
        this.enemySpeed = enemySpeed;
    }

    public void EnterState()
    {
        timer = 0;
        foreach (var item in projectile)
        {
            item.SetActive(true);
        } 
        currentPosition = new Vector3[2] {projectile[0].transform.localPosition, projectile[1].transform.localPosition};
        destPosition = new Vector3[2] {projectile[1].transform.localPosition, projectile[0].transform.localPosition};
    }

    public void ExitState()
    {
        foreach (var item in projectile)
        {
            item.SetActive(false);
        }  
        projectile[0].transform.localPosition = currentPosition[0];
        projectile[1].transform.localPosition = currentPosition[1];
    }

    public void UpdateState()
    {
        timer += Time.deltaTime;

        if (timer > skillDuration)
        {
            timer = 0;
            foreach (var item in projectile)
            {
                item.SetActive(false);
            }  
        }

        // projectile[0].transform.position = Vector3.MoveTowards(currentPosition[0], destPosition[0], Time.deltaTime * 5);    
        // projectile[1].transform.position = Vector3.MoveTowards(currentPosition[1], destPosition[1], Time.deltaTime * 5); 

        // if (Vector2.Distance(projectile[0].transform.position,destPosition[0]) < .1f)
        // {
        //     projectile[0].transform.localPosition = currentPosition[0];
        // }

        // if (Vector2.Distance(projectile[1].transform.position,destPosition[1]) < .1f)
        // {
        //     projectile[1].transform.localPosition = currentPosition[1];
        // }   
    
        projectile[0].transform.Translate(direction * Time.deltaTime * enemySpeed, Space.Self);
        projectile[1].transform.Translate(-direction * Time.deltaTime * enemySpeed, Space.Self);
    }
}
