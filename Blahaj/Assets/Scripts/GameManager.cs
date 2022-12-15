using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static GameState state = GameState.StartMenu;
    private static int level = 0;
    private static int enemiesLeft = 0;

    // Delegates and events
    public delegate void ChangeStateDelegate(GameState newState);
    public delegate GameState GetStateDelegate();
    public static ChangeStateDelegate ChangeStateEvent;
    public static GetStateDelegate GetStateEvent;

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
        ChangeStateEvent += ChangeState;
        GetStateEvent += GetState;
    }

    public void ChangeState(GameState newState) {
        state = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.StartMenu;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case GameState.StartMenu:
                // Start Menu
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
            case GameState.NewGame:
                // New Game
                // Reset level
                level = 0;
                if (SceneManager.GetActiveScene().name != GameStages.stages[level])
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(GameStages.stages[level]);
                    PlayerStats.ResetStatsEvent();
                }
                break;
            case GameState.InGame:
                // Main Game
                if (Time.timeScale == 0f)
                {
                    Time.timeScale = 1f;
                }
                // if (enemiesLeft == 0 && SceneManager.GetActiveScene().name != GameScenes.DigestionScene)
                // {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag(GameTags.Enemy);
                    enemiesLeft = enemies.Length;
                    if (enemiesLeft == 0)
                    {
                        SetGameState(GameState.WinLevel);
                    }
                // }
                break;
            case GameState.NextLevel:
                // Next Level
                level++;
                if (level >= GameStages.stages.Length)
                {
                    SetGameState(GameState.WinGame);
                }
                else
                {
                    SceneManager.LoadScene(GameStages.stages[level]);
                    Time.timeScale = 1f;
                    SetGameState(GameState.InGame);
                }
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
                if (SceneManager.GetActiveScene().name != GameScenes.DigestionScene)
                {
                    SceneManager.LoadScene(GameScenes.DigestionScene);
                }
                break;
            case GameState.Crafting:
                // Crafting State
                if (SceneManager.GetActiveScene().name != GameScenes.Crafting)
                {
                    SceneManager.LoadScene(GameScenes.Crafting);
                }
                break;
            case GameState.WinGame:
                Debug.Log("WinGame");
                if (SceneManager.GetActiveScene().name != GameScenes.WinGame)
                {
                    SceneManager.LoadScene(GameScenes.WinGame);
                }
                break;
            case GameState.LoseGame:
                Debug.Log("LoseGame");
                if (SceneManager.GetActiveScene().name != GameScenes.LoseGame)
                {
                    SceneManager.LoadScene(GameScenes.LoseGame);
                }
                break;
        }
    }

    public GameState GetState()
    {
        return state;
    }

    public void SetGameState(GameState newState) {
        state = newState;
    }

    /*public void RestartButton()
    {
        SetGameState(GameState.StartMenu);
        SceneManager.LoadScene("StartMenu");
    }*/

    

}
