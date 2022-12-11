using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Fields

    private List<string> skillsList = new List<string>(); // can use an abstract skills class as well
    [SerializeField] private int health;
    [SerializeField] private int attackDamage; // float?
    [SerializeField] private float attackSpeed; 
    
    private Dictionary<string, int> orbs = new Dictionary<string, int>(){
        {"Red", 0},
        {"Yellow", 0},
        {"Purple", 0}
    };
    // port movement speed over, have some way for playerMovement to access this value

    public delegate void TakeDamage(int damageAmount);
    public static TakeDamage DamageEvent;
    
    private SpriteRenderer mySR;

    #endregion
    
    #region Methods
    
    // Start is called before the first frame update
    private void Start()
    {
        PlayerStats.DamageEvent += Damage;
    }

    private void Awake()
    {
        this.mySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    
    private void Damage(int damageAmount)
    {
        this.health -= damageAmount;
        if (this.health < 0)
        {
            // some death animation
            PlayerStats.DamageEvent -= Damage;
            Destroy(this.gameObject);
        }
        StartCoroutine("FlashRedOnDamage"); // or have a more elaborate animation
    }

    private IEnumerator FlashRedOnDamage() {
        mySR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.15f);
        mySR.color = new Color(1, 1, 1, 1);

    }

    private float GetMovementSpeed()
    {
        // return movementSpeed through a getter that is broadcast through an event?
        return 0f;
    }

    private void ChangeMovementSpeed(float newMovementSpeed)
    {
        this.movementSpeed = newMovementSpeed;
    }

    private void ChangeAttackSpeed(float newAttackSpeed)
    {
        this.attackSpeed = newAttackSpeed;
    }

    private void ChangeAttackDamage(int newAttackDamage)
    {
        this.attackDamage = newAttackDamage;
    }

    private void RestoreHealth(int amountRestored)
    {
        this.health += amountRestored;
    }

    private void ConsumeOrbs(int red, int yellow, int purple)
    {
        // assuming that the value stored can never go below 0
        this.orbs["Red"] -= red;
        this.orbs["Yellow"] -= yellow;
        this.orbs["Purple"] -= purple;
    }

    private void GainOrbs(int red, int yellow, int purple)
    {
        // can combine this and the above into ChangeOrbs, just ensure the values passed in are the right sign
        this.orbs["Red"] += red;
        this.orbs["Yellow"] += yellow;
        this.orbs["Purple"] += purple;   
    }

    #endregion
}
