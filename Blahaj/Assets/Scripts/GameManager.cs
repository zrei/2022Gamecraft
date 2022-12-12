using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
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
                SceneManager.LoadScene("StartMenu");
                break;
            case GameState.NewGame:
                // New Game
                // Reset player stats

                // Reset level
                level = 1;
                SceneManager.LoadScene("Level" + level);
                break;
            case GameState.InGame:
                // Main Game
                Time.timeScale = 1f;
                break;
            case GameState.NextLevel:
                // Next Level
                level++;
                SceneManager.LoadScene("Level" + level);
                break;
            case GameState.PauseGame:
                // Pause Game
                Time.timeScale = 0f;
                break;
            case GameState.DigestionState:
                // Digestion State
                SceneManager.LoadScene("DigestionScene");
                break;
            case GameState.WinGame:
                Debug.Log("WinGame");
                SceneManager.LoadScene("WinGame");
                break;
            case GameState.LoseGame:
                Debug.Log("LoseGame");
                SceneManager.LoadScene("LoseGame");
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

}
