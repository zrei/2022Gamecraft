using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Constants;
using System;

public class FireballController : MonoBehaviour
{
    private Tuple<float, float, float> fireballInfo;
    private float movingLeft = 1f;
    private void Awake()
    {
        PlayerStats stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        this.fireballInfo = stats.GetSkillInfo(Skill.Fireball);
    }

    private void Update()
    {
       transform.position += transform.right * this.movingLeft * Time.deltaTime * fireballInfo.Item3;
    }

    public void SetMovingLeft()
    {
        this.movingLeft = -1f;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //Debug.Log("Hello!");
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fireballInfo.Item2);
            //Debug.Log(colliders.Length);
            foreach (var hitCollider in colliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    //Debug.Log("?");
                    hitCollider.SendMessage("Damage", fireballInfo.Item1);
                }
            }
            Destroy(this.gameObject);
        } else if (otherObject.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}