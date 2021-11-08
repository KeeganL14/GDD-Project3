using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu; 
    bool isPlaying = false;

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

    public void ActivatePauseMenu()
    {
        PauseMenu.SetActive(true);
        isPlaying = false;
    }
    public void ResumeGameButton()
    {
        PauseMenu.SetActive(false);
        isPlaying = true;
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
