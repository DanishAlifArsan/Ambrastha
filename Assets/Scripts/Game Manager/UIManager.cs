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
        }
    }

    private void gameOver() {
        Time.timeScale = 0;
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
            Time.timeScale = 0;
            enablePlayer(false);
        } else {
            Time.timeScale = 1;
            enablePlayer(true);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }
    public void TryAgain() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
    public void Quit() {
        Application.Quit();
    }
}