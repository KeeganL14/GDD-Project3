using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //scene transition 

public class GameManager : MonoBehaviour
{

    public bool isPlaying = false;
    private float playtime = 0.0f;
    private string timePlayed;

    public GameObject winMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public GameObject healthItemPrefab;
    public GameObject speedItemPrefab;
    public GameObject cooldownItemPrefab;
    public GameObject enemyPrefab;

    public GameObject player;

    public List<GameObject> enemiesInScene;
    public int numberOfEnemiesLeft;
    List<GameObject> itemsInScene;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;

        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        numberOfEnemiesLeft = enemiesInScene.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            playtime += Time.deltaTime;
            int hours = Mathf.FloorToInt(playtime / 3600F);
            int minutes = Mathf.FloorToInt((playtime % 3600) / 60);
            int seconds = Mathf.FloorToInt(playtime % 60);
            timePlayed = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            numberOfEnemiesLeft = enemiesInScene.Count;
            foreach (GameObject enemy in enemiesInScene)
            {
                if (enemy.activeInHierarchy != true)
                {
                    numberOfEnemiesLeft--;
                }
            }
            if (numberOfEnemiesLeft <= 0)
            {
                isPlaying = false;

                //if statement for scenes
                if(SceneManager.GetActiveScene().name == "Level 1")
                {
                    SceneManager.LoadScene("Level 2");
                }
                else if (SceneManager.GetActiveScene().name == "Level 2")
                {
                    SceneManager.LoadScene("Level 3");
                }
                else if (SceneManager.GetActiveScene().name == "Level 3")
                {
                    SceneManager.LoadScene("Level 4");
                }
                if (SceneManager.GetActiveScene().name == "Level 4")
                {
                    //SceneManager.LoadScene("boss");
                    ActivateWinMenu();
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)
                    && !winMenu.activeInHierarchy
                    && !gameOverMenu.activeInHierarchy)
        {
            isPlaying = !isPlaying; // toggle pause menu
            if (isPlaying == false)
                ActivatePauseMenu();
            else
                ResumeGameButton();
        }
    }

    public void ActivateWinMenu()
    {
        winMenu.SetActive(true);
        player.gameObject.SetActive(false);
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        //player.gameObject.SetActive(false);
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void ActivateGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        player.gameObject.SetActive(false);
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void ResumeGameButton()
    {
        isPlaying = true;
        Time.timeScale = 1;
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        player.gameObject.SetActive(true);
    }

    public void TryAgainButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1"); // reset scene
        ResumeGameButton();
    }

    public void QuitToMainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
