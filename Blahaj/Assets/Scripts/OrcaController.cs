using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaController : MonoBehaviour
{
    #region Fields

    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRadius;
    [SerializeField]private float minMovementSpeed;
    [SerializeField] private int numOrbsDropped;

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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        if (Vector3.Distance(transform.position, player.transform.position) < attackRadius) 
        {
            Debug.Log("Attack"); // must add countdown
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
                if (randomNumber == 0)
                {
                    Instantiate(RedOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                } else if (randomNumber == 1)
                {
                    Instantiate(YellowOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                } else 
                {
                    Instantiate(PurpleOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
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
