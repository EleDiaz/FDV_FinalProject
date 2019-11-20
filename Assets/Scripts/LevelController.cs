using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Canvas overlay;
    public Canvas pauseMenu;
    public Button menuButton;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button exitGame;

    // TODO: Add a timeSpeed to the delta component to control the pause in the game.
    //       Or maybe there are some other alternative pausing the tick event.
    void Start()
    {
        menuButton.onClick.AddListener(ToggleMenu);
        resumeButton.onClick.AddListener(ToggleMenu);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        exitGame.onClick.AddListener(ExitGame);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() { // Duplicate
        // TODO: Show a popup dialog
        // Convert this to a static method
        Application.Quit();
    }

    void Update()
    {
        
    }

    void ToggleMenu() {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }
}
