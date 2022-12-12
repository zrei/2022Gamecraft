using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainOrbs : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Orb"))
        {
            Debug.Log("Orb!");
            // call event from player stats
            // PlayerStats.GainOrbsEvent(red, yellow, purple);
            // must differentiate between three different types of orbs
            Destroy(otherObject.gameObject);
        }
    }
}
