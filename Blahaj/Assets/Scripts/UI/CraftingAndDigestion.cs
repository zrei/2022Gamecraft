using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class CraftingAndDigestion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButton() {
        GameManager.ChangeStateEvent(GameState.Crafting);
    }

    public void StomachButton() {
        GameManager.ChangeStateEvent(GameState.DigestionState);
    }
}
