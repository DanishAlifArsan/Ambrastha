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
    [SerializeField] private GameObject endDialogue;
    [SerializeField] private GameObject klana;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dewiSekartaji;
    [SerializeField] private GameObject dh;
    [SerializeField] private GameObject dh2;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dialogueMusic;
    [SerializeField] private AudioClip battleMusic;

    // Start is called before the first frame update
    private void Start()
    {
        klana.SetActive(false);
        StartCoroutine(BattleEntrance());
    }

    private void ShowDialogUI(GameObject dialogUI, bool isShow) {
        if (isShow)
        {
            audioSource.clip = dialogueMusic;
            audioSource.Play();
            uIManager.isInGame = false;
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            dialogUI.SetActive(true);
            battleUI.SetActive(false);
        } else {
            audioSource.clip = battleMusic;
            audioSource.Play();
            uIManager.isInGame = true;
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
        // audioSource.Play();

        if(klana.GetComponent<PlayerHealth>().dead && klana.GetComponent<PlayerHealth>().respawnCounter <= 0) {
            StartCoroutine(BattleEnd());
        }
        
        for (int i = 0; i < player.transform.childCount; i++)
            {
               if (player.transform.GetChild(i).GetComponent<PlayerHealth>() != null)
               {
                if (dewiSekartaji.activeInHierarchy)
                {
                    player.GetComponentInChildren<PlayerHealth>().damageReduction = true;
                } else {
                    player.GetComponentInChildren<PlayerHealth>().damageReduction = false;
                }
            }
        }
    }

    private IEnumerator BattleEnd() {
        ShowDialogUI(endDialogue,true);
        yield return new WaitUntil(() => !dh2.activeInHierarchy);
        ShowDialogUI(endDialogue, false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
    }
}
