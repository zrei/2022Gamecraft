using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class CraftingAndDigestion : MonoBehaviour
{
    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButton() {
        Debug.Log("BackButton");
        Debug.Log(GameManager.GetCachedState());
        GameManager.ChangeStateEvent(GameManager.GetCachedState());
    }

    public void StomachButton() {
        GameManager.SetCachedState(GameManager.GetStateEvent());
        GameManager.ChangeStateEvent(GameState.DigestionState);
    }
}
