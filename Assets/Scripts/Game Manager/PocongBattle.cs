using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PocongBattle : GameManager
{
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject endDialogue;
    [SerializeField] private GameObject pocong;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pocongPlaceholder;
    [SerializeField] private GameObject dh;
    [SerializeField] private GameObject dh1;
    [SerializeField] private GameObject dh2;
    [SerializeField] private GameObject hiddenWall;
    [SerializeField] private UIManager uIManager;

    private bool pocongToPosition = false;


    // Start is called before the first frame update
    private void Start()
    {
        LoadGame();
        uIManager.isInGame = false;
        pocong.SetActive(false);
        pocongPlaceholder.SetActive(false);
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        if (isTutorial)
        {
            StartCoroutine(MovementTutorial());
        } else {
            StartCoroutine(BattleEntrance());
        }
        
    }

    private void ShowDialogUI(GameObject dialogUI, bool isShow) {
        if (isShow)
        {
            uIManager.isInGame = false;
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            dialogUI.SetActive(true);
            battleUI.SetActive(false);
        } else {
            uIManager.isInGame = true;
            playerMovement.enabled = true;
            playerAttack.enabled = true;
            battleUI.SetActive(true);
            dialogUI.SetActive(false);
        }
    }

    private IEnumerator MovementTutorial() {
        // Debug.Log("Gunakan arah panah untuk bergerak");
        level = 2;
        SaveGame();
        ShowDialogUI(tutorialUI, true);
        yield return new WaitUntil(() => !dh1.activeInHierarchy);
        tutorialUI.SetActive(false);
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        uIManager.isInGame = true;
        yield return new WaitUntil(() => Vector3.Distance(player.transform.position, hiddenWall.transform.position) < 5f);
        yield return new WaitUntil(() => player.GetComponent<Rigidbody2D>().velocity == Vector2.zero);
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        uIManager.isInGame = false;
        isTutorial = false;
        SaveGame();
        StartCoroutine(BattleEntrance());
    }

    private IEnumerator BattleEntrance()
    {
        camera.GetComponent<CameraFollow>().enabled = false;
        camera.transform.position = new Vector3(0,0, -10);
        float temp = camera.GetComponent<Camera>().orthographicSize;
        float newSize = Mathf.MoveTowards(camera.GetComponent<Camera>().orthographicSize, 10, 2);
        camera.GetComponent<Camera>().orthographicSize = newSize;
        pocongPlaceholder.SetActive(true);
        pocongToPosition = true;
        ShowDialogUI(dialogUI,true);
        yield return new WaitUntil(() => !dh.activeInHierarchy);
        ShowDialogUI(dialogUI, false);
        pocongPlaceholder.SetActive(false);
        pocong.SetActive(true);
        camera.GetComponent<Camera>().orthographicSize = temp;
        camera.GetComponent<CameraFollow>().enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pocongToPosition)
        {
            pocongPlaceholder.transform.position = Vector3.MoveTowards(pocong.transform.position, pocongPlaceholder.transform.position, Time.deltaTime * 10);  
        }

        if(pocong.GetComponent<PlayerHealth>().dead && pocong.GetComponent<PlayerHealth>().respawnCounter <= 0) {
            StartCoroutine(BattleEnd());
            
        }
        
    }

    private IEnumerator BattleEnd() {
        ShowDialogUI(endDialogue,true);
        yield return new WaitUntil(() => !dh2.activeInHierarchy);
        ShowDialogUI(endDialogue, false);
        yield return new WaitForSeconds(1);
        level = 3;
        SaveGame();
        SceneManager.LoadScene(3);
    }
}
