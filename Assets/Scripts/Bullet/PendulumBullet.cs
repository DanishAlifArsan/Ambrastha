using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumBullet : MonoBehaviour
{
    [SerializeField] private float MaxAngleDeflection = 2.0f;
    [SerializeField] private float SpeedOfPendulum = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
        float angle = MaxAngleDeflection * Mathf.Sin( Time.time * SpeedOfPendulum);
        transform.Rotate(new Vector3(angle, 0, 0));
    }
}
