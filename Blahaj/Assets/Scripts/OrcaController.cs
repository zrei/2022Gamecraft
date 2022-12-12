using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaController : MonoBehaviour
{
    #region Fields

    [SerializeField] private int health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRadius;
    [SerializeField]private int minMovementSpeed;

    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private GameObject player;

    public delegate void TakeDamage(int damageAmount);
    public static TakeDamage DamageEvent;

    public delegate void Slow(int slowAmount);
    public static Slow SlowEvent;

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
        var step =  movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        if (Vector3.Distance(transform.position, player.transform.position) < attackRadius) 
        {
            Debug.Log("Attack"); // must add countdown
            //PlayerStats.Damage(this.attackDamage);
        }
    }

    private void Damage(int damageAmount)
    {
        this.health -= damageAmount;
        if (this.health <= 0)
        {
            // death anim
            OrcaController.DamageEvent -= Damage;
            OrcaController.SlowEvent -= SlowDown;
            Destroy(this.gameObject);
        }
    }

    private void SlowDown(int slowAmount)
    {
        this.movementSpeed = Mathf.Max(this.minMovementSpeed, this.minMovementSpeed - slowAmount);
    }

    #endregion
}
