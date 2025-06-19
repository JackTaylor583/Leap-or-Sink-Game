using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PauseMenu PauseMenu;

    public static bool GameIsPaused = false;

    public void PlayGame() // Loads game scene when clicked
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        SceneManager.LoadScene(1);
        GameStart();

    }

    public void QuitGame() // Exits application when clicked
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }

    
    public void GameStart() // Ensures the game starts not paused
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Start() // Required for the theme to play after quiting game
    {
      AudioManager.instance.Play("Theme");
    }
}
