using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    #region fields

    PlayerControls controls;
    PlayerStats stats;

    #endregion

    private void Awake()
    {
        this.controls = GetComponent<PlayerControls>();
        this.stats  = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        bool attacking = controls.getAttacking();
        float attackDamage = stats.GetAttackDamage();
        Debug.Log("Collision detected with " + otherObject.tag + "! Attacking: " + attacking);
        if (otherObject.CompareTag("Enemy") && attacking)
        {
            Debug.Log("Attacking Enemy!");
            otherObject.gameObject.GetComponent<EnemyController>().Damage(attackDamage);
        }
    }
}