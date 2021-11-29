using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused)
            {
                Resume();
                

            }
            else
            {
                Pause();
                
            }
        }   
    }

    public void Resume() {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Level0() {
        
        SceneManager.LoadScene("Level 0");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
       
    }

    public void Level1() {
       
        SceneManager.LoadScene("Level 1");
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        
    }

    public void Level2() {
        
        SceneManager.LoadScene("Level 2");
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        
    }

    void Pause() {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void LoadMainMenu() {
        Debug.Log("Main");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() {
        Debug.Log("Quiting");
        Application.Quit();

    }

    public void ResetGame() {
        Debug.Log("Reset Game");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }
}
