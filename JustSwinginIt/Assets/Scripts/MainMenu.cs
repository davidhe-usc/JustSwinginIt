using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onStartButton() {
        SceneManager.LoadScene("Level 1");
    }

    public void onAdvanceButton() {
        SceneManager.LoadScene("Level 2");
    }

    public void onTutorialButton() {
        SceneManager.LoadScene("Level 0");
    }

    public void onInstructionsButton() {
        SceneManager.LoadScene("Instructions");
    }
    
    public void onQuitButton() {
        Application.Quit();
        Debug.Log("Game quit!");
    }


}
