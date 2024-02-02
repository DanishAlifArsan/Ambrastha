using UnityEngine;

public class BantaranginLaserState : IState
{
    private GameObject[] projectile;
    private float skillDuration;
    private Vector3[] currentPosition, destPosition;
    float timer;

    public BantaranginLaserState(GameObject[] projectile, float skillDuration)
    {
        this.projectile = projectile;
        this.skillDuration = skillDuration;
    }

    public void EnterState()
    {
        timer = 0;
        foreach (var item in projectile)
        {
            item.SetActive(true);
        } 
        currentPosition = new Vector3[2] {projectile[0].transform.position, projectile[1].transform.position};
        destPosition = new Vector3[2] {projectile[1].transform.position, projectile[0].transform.position};
    }

    public void ExitState()
    {
        foreach (var item in projectile)
        {
            item.SetActive(false);
        }  
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

        if (Vector2.Distance(projectile[0].transform.position,destPosition[0]) < .1f)
        {
            projectile[0].transform.position = currentPosition[0];
        }

        if (Vector2.Distance(projectile[1].transform.position,destPosition[1]) < .1f)
        {
            projectile[1].transform.position = currentPosition[1];
        }   

        // projectile[0].transform.Translate(Vector3.left, Space.Self);
        // projectile[1].transform.Translate(Vector3.right, Space.Self);
    }
}
