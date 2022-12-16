using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Constants;
using System;

public class FireballController : MonoBehaviour
{
    private Tuple<float, float, float> explosionInfo;

    private void Awake()
    {
        PlayerStats stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        this.explosionInfo = stats.GetSkillInfo(Skill.Explosion);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * explosionInfo.Item3;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //Debug.Log("Hello!");
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionInfo.Item2);
            //Debug.Log(colliders.Length);
            foreach (var hitCollider in colliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    //Debug.Log("?");
                    hitCollider.SendMessage("Damage", explosionInfo.Item1);
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