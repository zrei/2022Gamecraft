using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    #region fields
   
    PlayerControls controls;

    #endregion

    private void Awake()
    {
        this.controls = GetComponent<PlayerControls>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        
        bool attacking = controls.getAttacking();
        float attackDamage = controls.getAttackDamage();
        if (otherObject.CompareTag("Enemy") && attacking)
        {
            Debug.Log("Attacking Enemy!");
            OrcaController orca = otherObject.gameObject.GetComponent<OrcaController>().DamageEvent(attackDamage);
        }
    }
}
