using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public bool isPlaying = false;
    private float playtime = 0.0f;
    private string timePlayed;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public GameObject healthItemPrefab;
    public GameObject speedItemPrefab;
    public GameObject cooldownItemPrefab;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;

        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
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
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.activeInHierarchy)
        { 
            isPlaying = !isPlaying; // toggle pause menu
            if (isPlaying == false)
                ActivatePauseMenu();
            else
                ResumeGameButton();
        }
    }

    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        // TODO:
        // - disable player movement and controls
        // - disable enemy movement
        // - pause any projectiles, particles, and/or animations
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void ResumeGameButton()
    {
        pauseMenu.SetActive(false);
        isPlaying = true;
        Time.timeScale = 1;
    }

    public void ActivateGameOverMenu()
    {
        isPlaying = false;
        player.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void TryAgainButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1"); // reset scene
    }

    public void QuitToMainMenuButton()
    {
        //TODO: add an "are you sure?" pop-up
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void QuitGameButton()
    {
        //TODO: add an "are you sure?" pop-up
        Application.Quit();
    }
}
