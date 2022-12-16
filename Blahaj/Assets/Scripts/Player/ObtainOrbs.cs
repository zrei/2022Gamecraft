using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainOrbs : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("PurpleOrb"))
        {
            //Debug.Log("Purple Orb!");
            Destroy(otherObject.gameObject);
            PlayerStats.ChangeOrbsEvent(0, 0, 1);
        } else if (otherObject.CompareTag("YellowOrb"))
        {
            //Debug.Log("Yellow orb");
            PlayerStats.ChangeOrbsEvent(0, 1, 0);
            Destroy(otherObject.gameObject);
        } else if (otherObject.CompareTag("RedOrb"))
        {
            //Debug.Log("Red orb");
            PlayerStats.ChangeOrbsEvent(1, 0, 0);
            Destroy(otherObject.gameObject);
        }
    }
}
