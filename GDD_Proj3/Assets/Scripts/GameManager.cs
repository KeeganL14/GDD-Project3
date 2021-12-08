using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu; 
    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;

        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        { // toggle pause menu
            if (isPlaying)
                ActivatePauseMenu();
            else
                ResumeGameButton();
        }
    }

<<<<<<< Updated upstream
=======
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

>>>>>>> Stashed changes
    public void ActivatePauseMenu()
    {
        PauseMenu.SetActive(true);
        // TODO:
        // - disable player movement and controls
        // - disable enemy movement
        // - pause any projectiles, particles, and/or animations
        isPlaying = false;
<<<<<<< Updated upstream
    }
=======
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

>>>>>>> Stashed changes
    public void ResumeGameButton()
    {
        PauseMenu.SetActive(false);
        isPlaying = true;
<<<<<<< Updated upstream
=======
        Time.timeScale = 1;
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene("Level 1"); // reset scene
>>>>>>> Stashed changes
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
