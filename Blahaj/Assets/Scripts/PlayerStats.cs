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
    [SerializeField] private float movementSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int minAttackDamage;
    [SerializeField] private int maxAttackDamage;
    [SerializeField] private float minAttackSpeed;
    [SerializeField] private float maxAttackSpeed;
    [SerializeField] private float minMovementSpeed;
    [SerializeField] private float maxMovementSpeed;
    
    private Dictionary<string, int> orbs = new Dictionary<string, int>(){
        {"Red", 0},
        {"Yellow", 0},
        {"Purple", 0}
    };
    // port movement speed over, have some way for playerMovement to access this value

    public delegate void IntStatsChange(int changeAmount);
    public static IntStatsChange IncreaseAttackDamageEvent;
    public static IntStatsChange DecreaseAttackDamageEvent;
    public static IntStatsChange RestoreHealthEvent;
    public static IntStatsChange DamageEvent;

    public delegate void FloatStatsChange(float changeAmount);
    public static FloatStatsChange IncreaseAttackSpeedEvent;
    public static FloatStatsChange DecreaseAttackSpeedEvent;
    public static FloatStatsChange IncreaseMovementSpeedEvent;
    public static FloatStatsChange DecreaseMovementSpeedEvent;

    public delegate float FloatGetter();
    public static FloatGetter MovementSpeedGetter;

    public delegate void OrbsChange(int red, int yellow, int purple);
    public static OrbsChange GainOrbsEvent;
    public static OrbsChange ConsumeOrbsEvent;

    /*
    public delegate void SkillGain(string skill);
    public static SkillGain GainSkillEvent;

    public delegate string SkillGetter(int index);
    public static SkillGetter RetrieveSkillEvent;
    */
    
    private SpriteRenderer mySR;

    #endregion
    
    #region Methods
    
    // Start is called before the first frame update
    private void Start()
    {
        PlayerStats.DamageEvent += Damage;
        PlayerStats.MovementSpeedGetter += GetMovementSpeed;
        PlayerStats.IncreaseAttackDamageEvent += IncreaseAttackDamage;
        PlayerStats.DecreaseAttackDamageEvent += DecreaseAttackDamage;
        PlayerStats.RestoreHealthEvent += RestoreHealth;
        PlayerStats.IncreaseAttackSpeedEvent += IncreaseAttackSpeed;
        PlayerStats.DecreaseAttackSpeedEvent += DecreaseAttackSpeed;
        PlayerStats.IncreaseMovementSpeedEvent += IncreaseMovementSpeed;
        PlayerStats.DecreaseMovementSpeedEvent += DecreaseMovementSpeed;
        PlayerStats.GainOrbsEvent += GainOrbs;
        PlayerStats.ConsumeOrbsEvent += ConsumeOrbs;
        /*
        PlayerStats.GainSkillEvent += GainSkill;
        PlayerStats.RetrieveSkillEvent += RetrieveSkill;
        */
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
            PlayerStats.MovementSpeedGetter -= GetMovementSpeed;
            PlayerStats.IncreaseAttackDamageEvent -= IncreaseAttackDamage;
            PlayerStats.DecreaseAttackDamageEvent -= DecreaseAttackDamage;
            PlayerStats.RestoreHealthEvent -= RestoreHealth;
            PlayerStats.IncreaseAttackSpeedEvent -= IncreaseAttackSpeed;
            PlayerStats.DecreaseAttackSpeedEvent -= DecreaseAttackSpeed;
            PlayerStats.IncreaseMovementSpeedEvent -= IncreaseMovementSpeed;
            PlayerStats.DecreaseMovementSpeedEvent -= DecreaseMovementSpeed;
            /*
            PlayerStats.GainSkillEvent -= GainSkill;
            PlayerStats.RetrieveSkillEvent -= RetrieveSkill;
            */
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
        return this.movementSpeed;
    }

    private void IncreaseMovementSpeed(float increaseAmount)
    {
        this.movementSpeed = Mathf.Min(this.maxMovementSpeed, this.movementSpeed + increaseAmount);
    }

    private void DecreaseMovementSpeed(float decreaseAmount)
    {
        this.movementSpeed = Mathf.Max(this.minMovementSpeed, this.movementSpeed - decreaseAmount);
    }

    private void IncreaseAttackSpeed(float increaseAmount)
    {
        this.attackSpeed = Mathf.Min(this.maxAttackSpeed, this.attackSpeed + increaseAmount);
    }

    private void DecreaseAttackSpeed(float decreaseAmount)
    {
        this.attackSpeed = Mathf.Max(this.minAttackSpeed, this.attackSpeed - decreaseAmount);
    }

    private void IncreaseAttackDamage(int increaseAmount)
    {
        this.attackDamage = Mathf.Min(this.maxAttackDamage, this.attackDamage + increaseAmount);
    }

    private void DecreaseAttackDamage(int decreaseAmount)
    {
        this.attackDamage = Mathf.Max(this.minAttackDamage, this.attackDamage - decreaseAmount);
    }

    private void RestoreHealth(int amountRestored)
    {
        this.health = Mathf.Min(this.maxHealth, this.health + amountRestored);
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
        this.orbs["Red"] += red;
        this.orbs["Yellow"] += yellow;
        this.orbs["Purple"] += purple;   
    }

    /*
    private void GainSkill(string skill) // replace with skill abstract class later?
    {
        this.skillsList.add(skill);
    }
    
    private string RetrieveSkill(int index)
    {
        if (index < skillsList.count)
        {
            return skillsList[index];
        } else
        {
            return null;
        }
    }
    */
    #endregion
}
