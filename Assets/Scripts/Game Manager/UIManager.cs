using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : GameManager
{
    [SerializeField] private GameObject player;
    [SerializeField] private WeaponSwitch weaponSwitch;
    [Header ("game over")]
    [SerializeField] private GameObject gameOverScreen;

    [Header ("pause")]
    [SerializeField] private GameObject pauseScreen;
    public bool isInGame = false;
    private bool isCanPause = true;
    private Animator pauseAnimator;

    private void Start()
    {
        if (isInGame)
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
            pauseAnimator = pauseScreen.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInGame)
        {
             if (Input.GetKeyDown(KeyCode.Escape) && isCanPause)
            {
                if(pauseScreen.activeInHierarchy) {
                    pauseGame(false);
                } else {
                    pauseGame(true);
                }  
            }
        
            if (!player.activeInHierarchy)
            {
                gameOver();
            }  

            if (pauseScreen.activeInHierarchy)
            {
                Time.timeScale = 0;
            } else if (gameOverScreen.activeInHierarchy) {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        } else {
            Time.timeScale = 1;
        }
    }

    private void gameOver() {
        enablePlayer(false);
        isCanPause = false;
        gameOverScreen.SetActive(true);

    }

    private void enablePlayer(bool status) {
        player.GetComponent<PlayerMovement>().enabled = status;
        player.GetComponent<PlayerAttack>().enabled = status;
        if (weaponSwitch != null)
        {
            weaponSwitch.enabled = status;
        }
    }

    private void pauseGame(bool status) {
        if(!status) {
            pauseAnimator.SetTrigger("exit");
        } else {
            pauseScreen.SetActive(true);
        }

        enablePlayer(!status);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }
    public void ContinueGame() {
        LoadGame();
        if (level != null)
        {
            SceneManager.LoadScene(level);
        }
    }
    public void Continue() {
        pauseGame(false);
    }
    public void TryAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
    public void Quit() {
        Application.Quit();
    }
}
