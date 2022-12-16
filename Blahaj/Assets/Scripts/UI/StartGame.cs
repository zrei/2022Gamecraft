using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class StartGame : MonoBehaviour
{

    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
    
    }
    // Start is called before the first frame update
    public void NewGame()
    {
        //Debug.Log("New Game");
        GameManager.ChangeStateEvent(GameState.NewGame);
    }
}
