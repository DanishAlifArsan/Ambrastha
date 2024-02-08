using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class BattleManager : GameManager
{
    [SerializeField] CameraFollow cam;
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyPlaceholder;
    [SerializeField] Flowchart flowchart;
    [SerializeField] GameObject skipButton;
    [SerializeField] UIManager uIManager;
    bool battleStart;

    public void BattleStart(bool status) {
        cam.enabled = status;
        playerAttack.enabled = status;
        playerMovement.enabled = status;
        battleUI.SetActive(status);
        battleStart = status;
        enemy.SetActive(status);
        enemyPlaceholder?.SetActive(!status);
        skipButton.SetActive(!status);
        uIManager.enabled = status;
    }

    public void BattleEnd() {
        cam.enabled = false;
        playerAttack.enabled = false;
        playerMovement.enabled = false;
        uIManager.enabled = false;  
        battleUI.SetActive(false);
    }

    public void StopDialogue() {
        flowchart.StopBlock("battle start");
        flowchart.ExecuteBlock("hide portrait");
        BattleStart(true);
    }

    private void Update() {
        if(battleStart && !enemy.activeInHierarchy) 
        {
            flowchart?.ExecuteBlock("battle end");    
        }
    }

}
