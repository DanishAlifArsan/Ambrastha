using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KlanaBattle : MonoBehaviour
{
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject pocong;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pocongPlaceholder;
    [SerializeField] private GameObject dh;
    [SerializeField] private GameObject dh1;

    private bool pocongToPosition = false;


    // Start is called before the first frame update
    private void Start()
    {
        Vector3 temp = player.transform.position;
        pocong.SetActive(false);
        pocongPlaceholder.SetActive(false);
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        StartCoroutine(MovementTutorial(temp));
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

    private IEnumerator MovementTutorial(Vector3 temp) {
        // Debug.Log("Gunakan arah panah untuk bergerak");
        ShowDialogUI(tutorialUI, true);
        yield return new WaitUntil(() => !dh1.activeInHierarchy);
        tutorialUI.SetActive(false);
        playerMovement.enabled = true;
        yield return new WaitUntil(() => Vector3.Distance(player.transform.position, temp) > .1f);
        yield return new WaitForSeconds(2);
        playerMovement.enabled = false;
        StartCoroutine(BattleEntrance());
    }

    private IEnumerator BattleEntrance()
    {
        pocongPlaceholder.SetActive(true);
        pocongToPosition = true;
        ShowDialogUI(dialogUI,true);
        yield return new WaitUntil(() => !dh.activeInHierarchy);
        ShowDialogUI(dialogUI, false);
        pocongPlaceholder.SetActive(false);
        pocong.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (pocongToPosition)
        {
            pocongPlaceholder.transform.position = Vector3.MoveTowards(pocong.transform.position, pocongPlaceholder.transform.position, Time.deltaTime * 10);  
        }
        
    }
}
