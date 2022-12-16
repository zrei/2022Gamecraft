using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OrcaController : MonoBehaviour, EnemyController
{
    #region Fields

    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRadius;
    [SerializeField] private float minMovementSpeed;
    [SerializeField] private int numOrbsDropped;
    [SerializeField] private float attackCooldown;
    private float lastAttackDamageTime;
    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private GameObject player;
    private PlayerStats playerStats;
    private Collider2D objectCollider;

    public delegate void ChangeStats(float damageAmount);
    public static ChangeStats DamageEvent;
    public static ChangeStats SlowEvent;
    public static ChangeStats StunEvent;

    public delegate void GetPoisoned(Tuple<float, float, float> poisonInfo);

    public static GetPoisoned PoisonEvent;

    [SerializeField] private GameObject RedOrb;
    [SerializeField] private GameObject YellowOrb;
    [SerializeField] public GameObject PurpleOrb;

    #endregion

    #region Methods

    private void Awake()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        this.mySR = GetComponent<SpriteRenderer>();
        this.player = GameObject.FindWithTag("Player");
        this.objectCollider = GetComponent<Collider2D>();
        OrcaController.DamageEvent += Damage;
        OrcaController.SlowEvent += SlowDown;
        this.objectCollider = GetComponent<Collider2D>();
        OrcaController.StunEvent += Stun;
        OrcaController.PoisonEvent += Poison;
    }

    // Update is called once per frame
    private void Update()
    {
        var step =  this.movementSpeed * Time.deltaTime; // calculate distance to move
        if (Time.time >= lastAttackDamageTime + attackCooldown)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            float x = Mathf.Abs(transform.localScale.x);
            float y = transform.localScale.y;
            float z = transform.localScale.z;
            if (player.transform.position.x - transform.position.x >= 0) {
                transform.localScale = new Vector3(-x, y, z);
                //mySR.flipX = true;
            } else if (player.transform.position.x - transform.position.x < 0) {
                transform.localScale = new Vector3(x, y, z);
                //mySR.flipX = false;
            }
        }
        if ((Vector3.Distance(transform.position, player.transform.position) < attackRadius) && (Time.time >= lastAttackDamageTime + attackCooldown))
        {
            lastAttackDamageTime = Time.time;
            Debug.Log("Attack");
            PlayerStats.DamageEvent(this.attackDamage);
        }
        /*
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        if (Vector3.Distance(transform.position, player.transform.position) < attackRadius) 
        {
            //Debug.Log("Attack"); // must add countdown
            //PlayerStats.Damage(this.attackDamage);
        }*/
    }

    public void Damage(float damageAmount)
    {
        Debug.Log("Orca damaged");
        this.health -= damageAmount;
        if (this.health <= 0)
        {
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
            // death anim     
            Destroy(this.gameObject);
        }
    }

    private void SlowDown(float slowAmount)
    {
        this.movementSpeed = Mathf.Max(this.minMovementSpeed, this.minMovementSpeed - slowAmount);
    }

    private void OnDestroy()
    {
        OrcaController.DamageEvent -= Damage;
        OrcaController.SlowEvent -= SlowDown;
        OrcaController.StunEvent -= Stun;
        OrcaController.PoisonEvent -= Poison;
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
        float baseMovementSpeed = this.movementSpeed;
        this.movementSpeed = 0;
        float time = 0f;
        float interval = 0.5f;
        while (time < stunTime)
        {
            yield return new WaitForSeconds(interval);
            time += interval;
        }
        mySR.color = new Color(1.0f, 1.0f, 1.0f);
        this.movementSpeed = baseMovementSpeed;
    }

    private void Poison(Tuple<float, float, float> poisonInfo)
    //float poisonDamage, float damageInterval, float damageTime)
    {
        StartCoroutine(PoisonDamage(poisonInfo.Item1, poisonInfo.Item2, poisonInfo.Item3));
    }

    private void Stun(float stunTime)
    {
        StartCoroutine(StunTime(stunTime));
    }

    #endregion
}
