using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static GameState state;
    private static int level;

    void Awake()
    {
        // Debug.Log("awake!");
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.StartMenu;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case GameState.StartMenu:
                // Start Menu
                if (SceneManager.GetActiveScene().name != "StartMenu")
                {
                    SceneManager.LoadScene("StartMenu");
                }
                break;
            case GameState.NewGame:
                // New Game
                // Reset player stats
                PlayerStats.ResetStatsEvent();
                // Reset level
                level = 1;
                SceneManager.LoadScene("Game" + level);
                Time.timeScale = 1f;
                SetGameState(GameState.InGame);
                break;
            case GameState.InGame:
                // Main Game
                if (Time.timeScale == 0f)
                {
                    Time.timeScale = 1f;
                }
                break;
            case GameState.NextLevel:
                // Next Level
                level++;
                SceneManager.LoadScene("Game" + level);
                Time.timeScale = 1f;
                SetGameState(GameState.InGame);
                break;
            case GameState.PauseGame:
                // Pause Game
                if (Time.timeScale == 1f)
                {
                    Time.timeScale = 0f;
                }
                break;
            case GameState.DigestionState:
                // Digestion State
                if (SceneManager.GetActiveScene().name != "DigestionScene")
                {
                    SceneManager.LoadScene("DigestionScene");
                }
                break;
            case GameState.WinGame:
                Debug.Log("WinGame");
                if (SceneManager.GetActiveScene().name != "WinGame")
                {
                    SceneManager.LoadScene("WinGame");
                }
                break;
            case GameState.LoseGame:
                Debug.Log("LoseGame");
                if (SceneManager.GetActiveScene().name != "LoseGame")
                {
                    SceneManager.LoadScene("LoseGame");
                }
                break;
        }
    }

    public GameState GetGameState()
    {
        return state;
    }

    public void SetGameState(GameState newState) {
        state = newState;
    }

    public void RestartButton()
    {
        SetGameState(GameState.StartMenu);
        SceneManager.LoadScene("StartMenu");
    }

}
