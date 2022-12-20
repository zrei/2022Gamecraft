using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject helpMenu;
    private static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    // Update is called once per frame
    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed");
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }*/

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        GameManager.ChangeStateEvent(GameState.PauseGame);
        //Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        CloseHelp();
        GameManager.ChangeStateEvent(GameState.InGame);
        //Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        GameManager.ChangeStateEvent(GameState.StartMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void HelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void CloseHelp()
    {
        helpMenu.SetActive(false);
    }
}
