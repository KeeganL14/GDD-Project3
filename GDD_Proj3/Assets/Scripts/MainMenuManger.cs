using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManger : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void StartButton()
    {
        // initialize game
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }
    public void CreditsButton()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void MainMenuButton()
    {
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
