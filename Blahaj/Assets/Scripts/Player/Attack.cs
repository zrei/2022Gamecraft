using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    #region fields

    PlayerControls controls;
    PlayerStats stats;
    private Rigidbody2D rb2D;

    #endregion

    private void Awake()
    {
        this.controls = GetComponent<PlayerControls>();
        this.stats  = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        this.rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        Debug.Log("Collision detected with " + otherObject.gameObject.tag + "!");
        collisionTrigger(otherObject);
    }

    private void OnCollisionStay2D(Collision2D otherObject)
    {
        Debug.Log("Collision stay with " + otherObject.gameObject.tag + "!");
        collisionTrigger(otherObject);
    }

    // private void OnTriggerEnter2D(Collision2D otherObject)
    // {
    //     collisionTrigger(otherObject);
    // }

    private void collisionTrigger(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        bool attacking = controls.getAttacking();
        float attackDamage = stats.GetAttackDamage();
        Debug.Log("Collision detected with " + otherObject.tag + "! Attacking: " + attacking);
        Debug.Log(rb2D.velocity);
        if (otherObject.CompareTag("Enemy"))
        {
            EnemyController enemy = otherObject.gameObject.GetComponent<EnemyController>();
            if (attacking)
            {
                Debug.Log("Attacking Enemy!");
                otherObject.gameObject.GetComponent<EnemyController>().Damage(attackDamage);
            }
            if (enemy.isAttacking())
            {
                PlayerStats.DamageEvent(enemy.getAttackDamage());
                Rigidbody2D enemyRB2D = otherObject.gameObject.GetComponent<Rigidbody2D>();
                Debug.Log(enemyRB2D.velocity);
            }
        }
    }
}