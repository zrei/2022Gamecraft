using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orca2Controller : MonoBehaviour
{
    #region Fields

    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRadius;
    [SerializeField]private float minMovementSpeed;
    [SerializeField] private int numOrbsDropped;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float lastAttackDamageTime;

    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private GameObject player;

    public delegate void ChangeStats(float damageAmount);
    public static ChangeStats DamageEvent;
    public static ChangeStats SlowEvent;

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
        OrcaController.DamageEvent += Damage;
        OrcaController.SlowEvent += SlowDown;
    }

    // Update is called once per frame
    private void Update()
    {
        var step =  this.movementSpeed * Time.deltaTime; // calculate distance to move
        Quaternion rotationZ = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*Mathf.Atan(player.transform.position.y / player.transform.position.x));                
            transform.rotation = rotationZ;
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
            
        if ((Vector3.Distance(transform.position, player.transform.position) < attackRadius) && (Time.time >= lastAttackDamageTime + attackCooldown))
        {
            lastAttackDamageTime = Time.time;
            Debug.Log("Attack");
            //PlayerStats.Damage(this.attackDamage);
        }
    }

    private void Damage(float damageAmount)
    {
        this.health -= damageAmount;
        if (this.health <= 0)
        {
            for (int i = 0; i < numOrbsDropped; i++)
            {
                int randomNumber = Random.Range(0, 3);
                Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-2.0f, 2.0f), 
                    transform.position.y + Random.Range(-2.0f, 2.0f), 
                    transform.position.z + Random.Range(-2.0f, 2.0f));
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
    }

    #endregion
}
