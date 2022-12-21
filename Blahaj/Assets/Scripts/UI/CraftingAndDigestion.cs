using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class CraftingAndDigestion : MonoBehaviour
{
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
    }

    public void BackButton() {
        Debug.Log("BackButton");
        GameManager.ChangeStateEvent(GameState.WinLevel);
        GameObject digestionMenu = GameObject.FindWithTag("Digestion");
        digestionMenu.transform.GetChild(0).gameObject.SetActive(false);
        //Debug.Log(GameManager.GetCachedState());
        //GameManager.ChangeStateEvent(GameManager.GetCachedState());
    }

    public void StomachButton() {
        //DigestionMenu();
        //GameManager.SetCachedState(GameManager.GetStateEvent());
        //GameManager.ChangeStateEvent(GameState.DigestionState);
    }

    public void DigestionMenu()
    {
        GameManager.ChangeStateEvent(GameState.DigestionState);
        GameObject digestionMenu = GameObject.FindWithTag("Digestion");
        digestionMenu.transform.GetChild(0).gameObject.SetActive(true);
        DigestControls.ResetDisplayEvent();
    }
}
