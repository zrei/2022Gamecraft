using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using Constants;

public class MainMenu : MonoBehaviour
{
    public GameObject helpMenu;
    private bool isHelp;
    // Start is called before the first frame update
    void Start()
    {
        helpMenu.SetActive(false);
    }

    /*public void StartButton()
    {
        SceneManager.LoadScene(0);
    }*/

    public void StartButton()
    {
        Debug.Log("New Game");
        GameManager.ChangeStateEvent(GameState.NewGame);
    }


    public void HelpButton()
    {
        helpMenu.SetActive(true);
        isHelp = true;
    }

    public void CloseHelp()
    {
        helpMenu.SetActive(false);
        isHelp = false;
    }

    public void QuitButton()
    {
        Debug.Log("ayy lmao quit");
        Application.Quit();
    }
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isHelp)
            {
                CloseHelp();
            }
        }
    }
}
