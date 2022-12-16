using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShrimpController : MonoBehaviour
{
    #region Fields
    private GameObject player;
    [SerializeField] private int health;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float minShootCountdown;
    [SerializeField] private float maxShootCountdown;
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
        this.shootCountdown = Random.Range(minShootCountdown, maxShootCountdown);
    }

    public void Damage(int damageAmount)
    {
        Debug.Log(damageAmount);
        health -= damageAmount;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
        StartCoroutine("FlashRedOnDamage");
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

    #endregion
    
}