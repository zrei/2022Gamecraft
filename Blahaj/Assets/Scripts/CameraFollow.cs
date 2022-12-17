using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  
  public Transform target; 

  //Affects speed camera follows player  
  public float smoothSpeed = 0.125f;

  //camera snaps to inside player by default. Offset z pos to fix view
  public Vector3 offset; 

  void FixedUpdate() {

    Vector3 desiredPosition = target.position + offset; 
    Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPos;

    //transform.LookAt(target);

  }

}
