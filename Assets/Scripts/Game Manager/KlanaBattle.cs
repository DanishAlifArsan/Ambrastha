using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KlanaBattle : MonoBehaviour
{
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject dialogUI;
    // [SerializeField] private GameObject endDialogue;
    [SerializeField] private GameObject klana;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dh;
    // [SerializeField] private GameObject dh2;



    // Start is called before the first frame update
    private void Start()
    {
        klana.SetActive(false);
        StartCoroutine(BattleEntrance());
    }

    private void ShowDialogUI(GameObject dialogUI, bool isShow) {
        if (isShow)
        {
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            dialogUI.SetActive(true);
            battleUI.SetActive(false);
        } else {
            playerMovement.enabled = true;
            playerAttack.enabled = true;
            battleUI.SetActive(true);
            dialogUI.SetActive(false);
        }
    }

    private IEnumerator BattleEntrance()
    {
        ShowDialogUI(dialogUI,true);
        yield return new WaitUntil(() => !dh.activeInHierarchy);
        klana.SetActive(true);
        ShowDialogUI(dialogUI, false);
    }

    // Update is called once per frame
    private void Update()
    {   
        if(klana.GetComponent<PlayerHealth>().dead && klana.GetComponent<PlayerHealth>().respawnCounter <= 0) {
            StartCoroutine(BattleEnd());
        }
    }

    private IEnumerator BattleEnd() {
        // ShowDialogUI(endDialogue,true);
        // yield return new WaitUntil(() => !dh2.activeInHierarchy);
        // ShowDialogUI(endDialogue, false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
    }
}
