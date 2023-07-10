using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private Transform player;
   [SerializeField] private float cameraSpeed;
   [SerializeField] Vector3 minValue, maxValue;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 boundPosition = new Vector3(Mathf.Clamp(player.position.x, minValue.x,maxValue.x),
        Mathf.Clamp(player.position.y, minValue.y,maxValue.y), Mathf.Clamp(player.position.z, minValue.z,maxValue.z));

        transform.position = Vector3.Lerp(transform.position, boundPosition, Time.deltaTime * cameraSpeed);
    }
}
