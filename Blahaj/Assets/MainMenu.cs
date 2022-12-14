using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Debug.Log("ayy lmao quit");
        //Application.Quit();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
