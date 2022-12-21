using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class CraftingMenu : MonoBehaviour
{
    private List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            list.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetStateEvent() == GameState.WinLevel || GameManager.GetStateEvent() == GameState.DigestionState)
        {
            foreach (GameObject child in list)
            {
                child.SetActive(true);
            }
            // Debug.Log("WinLevel");
        } else if (gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            foreach (GameObject child in list)
            {
                child.SetActive(false);
            }
            // Debug.Log("Not WinLevel");
        }
    }
}
