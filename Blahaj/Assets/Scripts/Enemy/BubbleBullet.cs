using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleBullet : MonoBehaviour
{
    #region Fields

    [SerializeField] private float bulletSpeed;

    [SerializeField] private int baseDamage;

    #endregion

    #region Methods

    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        } else if (otherObject.gameObject.CompareTag("Player"))
        {
            PlayerStats.DamageEvent(baseDamage);
            Destroy(this.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    #endregion
}