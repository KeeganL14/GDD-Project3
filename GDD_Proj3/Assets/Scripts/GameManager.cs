using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        //instantiate enemies
        GameObject enemy = Instantiate(enemyPrefab);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemy.transform.position = new Vector3(-4.45f, -2.77f, 0.0f);        
        enemyScript.canShoot = false;
        enemyScript.shootCooldown = 0.75f;
        enemyScript.chaseSpeed = 10.0f;        
        enemyScript.followDistance = 6.0f;
        enemyScript.damage = 15.0f;
        enemyScript.health = 25.0f;
        enemyScript.defense = 5.0f;
        enemyScript.itemDropRate = 8.0f;
        enemyScript.targetPoint = player.transform;
        enemiesInScene.Add(enemy);

        enemy = Instantiate(enemyPrefab);
        enemyScript = enemy.GetComponent<Enemy>();
        enemy.transform.position = new Vector3(5.93f, 3.24f, 0.0f);        
        enemyScript.canShoot = true;
        enemyScript.shootCooldown = 1.75f;
        enemyScript.chaseSpeed = 10.0f;
        enemyScript.followDistance = 8.0f;
        enemyScript.damage = 10.0f;
        enemyScript.health = 35.0f;
        enemyScript.defense = 12.0f;
        enemyScript.itemDropRate = 5.0f;
        enemyScript.targetPoint = player.transform;
        enemiesInScene.Add(enemy);

        enemy = Instantiate(enemyPrefab);
        enemyScript = enemy.GetComponent<Enemy>();
        enemy.transform.position = new Vector3(6.86f, -3.28f, 0.0f);        
        enemyScript.canShoot = true;
        enemyScript.shootCooldown = 0.75f;
        enemyScript.chaseSpeed = 5.5f;
        enemyScript.followDistance = 6.0f;
        enemyScript.damage = 25.0f;
        enemyScript.health = 50.0f;
        enemyScript.defense = 10.0f;
        enemyScript.itemDropRate = 10.0f;
        enemyScript.targetPoint = player.transform;
        enemiesInScene.Add(enemy);

        numberOfEnemiesLeft = enemiesInScene.Count;
    }

    private void OnGUI()
    {
        // !!!for debug purposes!!!
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 18;

        if (isPlaying)
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
        winMenu.SetActive(true);
        player.gameObject.SetActive(false);
        isPlaying = false;
    }

    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        player.gameObject.SetActive(false);
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void ActivateGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        player.gameObject.SetActive(false);
        isPlaying = false;
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
