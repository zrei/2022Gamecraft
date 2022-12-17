using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Constants;
using System;

public class FireballController : MonoBehaviour
{
    private Tuple<float, float, float> fireballInfo;

    private void Awake()
    {
        PlayerStats stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        this.fireballInfo = stats.GetSkillInfo(Skill.Fireball);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * fireballInfo.Item3;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //Debug.Log("Hello!");
        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("fireballenemyhit");
            otherObject.GetComponent<EnemyController>().Damage(fireballInfo.Item1);
            // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fireballInfo.Item2);
            // //Debug.Log(colliders.Length);
            // foreach (var hitCollider in colliders)
            // {
            //     if (hitCollider.CompareTag("Enemy"))
            //     {
            //         Debug.Log("?");
            //         hitCollider.GetComponent<EnemyController>().Damage(fireballInfo.Item1);
            //     }
            // }
            // Destroy(this.gameObject);
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