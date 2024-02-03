using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - transform.position.x > 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
