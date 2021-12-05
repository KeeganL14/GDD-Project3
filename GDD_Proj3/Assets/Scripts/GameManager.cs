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
    public GameObject enemyPrefab;

    public GameObject player;

    List<GameObject> enemiesInScene;
    List<GameObject> itemsInScene;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;

        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void OnGUI()
    {
        // !!!for debug purposes!!!
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 18;

        GUI.Box(new Rect(0, 100, 100, 30), timePlayed);       

        GUI.skin.box.wordWrap = true;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
