﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1;
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

                ActivateWinMenu();
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
        isPlaying = false;
        Time.timeScale = 0;

        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            ActivateGameWin();
            return;
        }

        winMenu.SetActive(true);
    }

    public void ActivatePauseMenu()
    {
        isPlaying = false;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ActvateGameOverMenu()
    {
        isPlaying = false;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    private void ActivateGameWin()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevelButton()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level 1":
                SceneManager.LoadScene("Level 2");
                break;
            case "Level 2":
                SceneManager.LoadScene("Level 3");
                break;
            case "Level 3":
                SceneManager.LoadScene("Level 4");
                break;
            case "Level 4":
                SceneManager.LoadScene("Level 5");
                break;
            case "Level 5":
                SceneManager.LoadScene("Main Menu");
                break;
        }

        /*
        switch (player.GetComponent<PlayerCharacter>().currentLevel)
        {
            case 1:
                SceneManager.LoadScene("Level 2");
                break;
            case 2:
                SceneManager.LoadScene("Level 3");
                break;
            case 3:
                SceneManager.LoadScene("Level 4");
                break;
            case 4:
                SceneManager.LoadScene("Level 5");
                break;
            case 5:
                SceneManager.LoadScene("Main Menu");
                break;
        }
        */
    }

    public void ResumeGameButton()
    {
        isPlaying = true;
        Time.timeScale = 1;
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reset scene
    }

    public void QuitToMainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
