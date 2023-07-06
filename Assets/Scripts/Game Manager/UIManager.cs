using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private WeaponSwitch weaponSwitch;
    [Header ("game over")]
    [SerializeField] private GameObject gameOverScreen;
    // [SerializeField] private AudioClip gameOverSound;

    [Header ("pause")]
    [SerializeField] private GameObject pauseScreen;
    public bool isInGame = false;
    // [SerializeField] private AudioClip pauseSound;
    // Start is called before the first frame update
    private void Start()
    {
        if (isInGame)
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInGame)
        {
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(pauseScreen.activeInHierarchy) {
                    pauseGame(false);
                } else {
                    pauseGame(true);
                }  
            }
        

            if (player.GetComponentInChildren<PlayerHealth>().dead)
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
        gameOverScreen.SetActive(true);
        // SoundManager.instance.playSound(gameOverSound);
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
        pauseScreen.SetActive(status);
        

        if(status) {
            enablePlayer(false);
        } else {
            enablePlayer(true);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
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
