using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShrimpController : MonoBehaviour, EnemyController
{
    #region Fields
    private GameObject player;
    [SerializeField] private float health;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float minShootCountdown;
    [SerializeField] private float maxShootCountdown;
    [SerializeField] private int numOrbsDropped;
    [SerializeField] private GameObject RedOrb;
    [SerializeField] private GameObject YellowOrb;
    [SerializeField] public GameObject PurpleOrb;
    private float shootCountdown;
    private bool activated = false;

    private SpriteRenderer mySR;
    //private new CircleCollider2D collider;
    #endregion

    #region Methods
    private void Awake()
    {
        mySR = GetComponent<SpriteRenderer>();
        //collider = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ResetShootCountdown();
    }

    // Update is called once per frame
    private void Update()
    {
        if (activated)
        {
            shootCountdown -= Time.deltaTime;
            if (shootCountdown <= 0) {
                Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
                Instantiate(enemyBullet, transform.position, bulletRotation);
                ResetShootCountdown();
            }
        }
        
    }

    private void ResetShootCountdown()
    {
        this.shootCountdown = UnityEngine.Random.Range(minShootCountdown, maxShootCountdown);
    }

    public void Damage(float damageAmount)
    {
        Debug.Log("Shrimp Damaged: " + damageAmount);
        health -= damageAmount;
        if (health <= 0) {
             for (int i = 0; i < numOrbsDropped; i++)
            {
                int randomNumber = UnityEngine.Random.Range(0, 3);
                Vector3 spawnPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(-2.0f, 2.0f), 
                    transform.position.y + UnityEngine.Random.Range(-2.0f, 2.0f), 
                    transform.position.z + UnityEngine.Random.Range(-2.0f, 2.0f));
                switch (randomNumber)
                {
                    case (0):
                        Instantiate(RedOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                        break;
                    case (1):
                        Instantiate(YellowOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                        break;
                    case (2):
                        Instantiate(PurpleOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                        break;
                }
            }
            Destroy(this.gameObject);
        }
        //StartCoroutine("FlashRedOnDamage");
    }

    private IEnumerator FlashRedOnDamage() {
        mySR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.15f);
        mySR.color = new Color(1, 1, 1, 1);
    }

    private void OnBecameVisible() {
        if (!activated)
        {
            activated = true;
            ResetShootCountdown();
        }
    }

    private void OnBecameInvisible()
    {
        activated = false;
    }

    private IEnumerator PoisonDamage(float poisonDamage, float damageInterval, float damageTime)
    {
        mySR.color = new Color(1.0f, 0f, 1.0f);
        float time = 0;
        while (time < damageTime)
        {
            Damage(poisonDamage);
            yield return new WaitForSeconds(damageInterval);
            time += damageInterval;
        }
        mySR.color = new Color(1.0f, 1.0f, 1.0f);
    }

    private IEnumerator StunTime(float stunTime)
    {
        mySR.color = new Color(1.0f, 1.0f, 0f);
        activated = false;
        yield return new WaitForSeconds(stunTime);
        mySR.color = new Color(1.0f, 1.0f, 1.0f);
        activated = true;
    }

    private void Poison(Tuple<float, float, float> poisonInfo)
    //float poisonDamage, float damageInterval, float damageTime)
    {
        StartCoroutine(PoisonDamage(poisonInfo.Item1, poisonInfo.Item2, poisonInfo.Item3));
    }

    private void Stun(float stunTime)
    {
        //Debug.Log("Shrimp stunned!");
        StartCoroutine(StunTime(stunTime));
    }

    #endregion
    
}