using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BackgroundScroll : MonoBehaviour
{
   [SerializeField]
   private Transform centerBG;
   private float offset = 40f; 

    void Update() {

        if (transform.position.x >= centerBG.position.x + offset) {
            centerBG.position = new Vector2(transform.position.x + offset, centerBG.position.y);
        } else if (transform.position.x <= centerBG.position.x - offset) {
            centerBG.position = new Vector2(transform.position.x - offset, centerBG.position.y);
        }
   }
}
