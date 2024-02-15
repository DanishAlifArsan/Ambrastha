using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : GameManager
{
    [SerializeField] GameObject[] dialogueUI;
    [SerializeField] GameObject[] dh;
    
    // Start is called before the first frame update
    private void Start()
    {
        dialogueUI[0].SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (dialogueUI[0].activeInHierarchy)
        {
            if (!dh[0].activeInHierarchy)
            {
                dialogueUI[0].SetActive(false);
            }
        } else {
            dialogueUI[1].SetActive(true);
            if (!dh[1].activeInHierarchy)
            {
                dialogueUI[1].SetActive(false);
                SceneManager.LoadScene(0);
            }
        }   

        
    }
}
