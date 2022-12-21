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
    private static GameState cachedState;
    private static int level = 0;
    private static int enemiesLeft = 0;
    private static List<Tuple<Orbs, Vector2>> cachedOrbs;
    private static Vector2 cachedPlayerPosition;
    private static Quaternion cachedPlayerRotation;

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
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void onDestory()
    {
        ChangeStateEvent -= ChangeState;
        GetStateEvent -= GetState;
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void ChangeState(GameState newState) {
        state = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
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
            case GameState.WinLevel:
                // Win Level
                if (Time.timeScale == 0f)
                {
                    Time.timeScale = 1f;
                }
                if (SceneManager.GetActiveScene().name != GameStages.stages[level])
                {
                    SceneManager.LoadScene(GameStages.stages[level]);
                }
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
                /*if (SceneManager.GetActiveScene().name != GameScenes.DigestionScene)
                {
                    Debug.Log("DigestionState");
                    cachedOrbs = new List<Tuple<Orbs, Vector2>>();
                    foreach (GameObject orb in GameObject.FindGameObjectsWithTag(GameTags.RedOrb))
                    {
                        cachedOrbs.Add(new Tuple<Orbs, Vector2>(Orbs.RedOrb, orb.transform.position));
                    }
                    foreach (GameObject orb in GameObject.FindGameObjectsWithTag(GameTags.PurpleOrb))
                    {
                        cachedOrbs.Add(new Tuple<Orbs, Vector2>(Orbs.PurpleOrb, orb.transform.position));
                    }
                    foreach (GameObject orb in GameObject.FindGameObjectsWithTag(GameTags.YellowOrb))
                    {
                        cachedOrbs.Add(new Tuple<Orbs, Vector2>(Orbs.YellowOrb, orb.transform.position));
                    }
                    Transform cachedPlayerTransform = GameObject.FindGameObjectWithTag(GameTags.Player).transform;
                    cachedPlayerPosition = cachedPlayerTransform.position;
                    cachedPlayerRotation = cachedPlayerTransform.rotation;
                    SceneManager.LoadScene(GameScenes.DigestionScene);
                }*/
                if (Time.timeScale == 1f)
                {
                    Time.timeScale = 0f;
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
                // Debug.Log("WinGame");
                if (SceneManager.GetActiveScene().name != GameScenes.WinGame)
                {
                    SceneManager.LoadScene(GameScenes.WinGame);
                }
                break;
            case GameState.LoseGame:
                //Debug.Log("LoseGame");
                if (SceneManager.GetActiveScene().name != GameScenes.LoseGame)
                {
                    SceneManager.LoadScene(GameScenes.LoseGame);
                }
                break;
        }
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (state == GameState.WinLevel)
        {
            Debug.Log("WinLevelNext");
            Debug.Log(cachedOrbs);
            GameObject player = GameObject.FindWithTag(GameTags.Player);
            player.transform.position = cachedPlayerPosition;
            player.transform.rotation = cachedPlayerRotation;
            foreach (Tuple<Orbs, Vector2> orb in cachedOrbs)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Orbs/" + orb.Item1.ToString()), orb.Item2, Quaternion.identity);
            }
            GameObject enemies = GameObject.FindWithTag(GameTags.Enemies);
            foreach (Transform enemy in enemies.transform)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    public GameState GetState()
    {
        return state;
    }

    public void SetGameState(GameState newState) {
        state = newState;
    }

    public static void SetCachedState(GameState newState) {
        cachedState = newState;
    }

    public static GameState GetCachedState() {
        return cachedState;
    }

    /*public void RestartButton()
    {
        SetGameState(GameState.StartMenu);
        SceneManager.LoadScene("StartMenu");
    }*/

    

}
