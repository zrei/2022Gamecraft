using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PortalLogic : MonoBehaviour
{
    private GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
       portal = gameObject.transform.GetChild(0).gameObject;
       portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetStateEvent() == GameState.WinLevel)
        {
            portal.SetActive(true);
        }
        else if (GameManager.GetStateEvent() != GameState.WinLevel)
        {
            portal.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && GameManager.GetStateEvent() == GameState.WinLevel)
        {
            portal.SetActive(false);
            GameManager.ChangeStateEvent(GameState.NextLevel);
        }
    }
}
