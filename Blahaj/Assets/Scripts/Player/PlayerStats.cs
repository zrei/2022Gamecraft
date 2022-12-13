using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
public class PlayerStats : MonoBehaviour
{

    #region Fields

    private List<Skill> skillsList = new List<Skill>();
    private float health;
    [SerializeField] private float baseAttackDamage;
    private float attackDamage;
    [SerializeField] private float baseAttackSpeed; 
    private float attackSpeed;
    [SerializeField] private float baseMovementSpeed;
    private float movementSpeed;
    [SerializeField] private float baseMaxHealth;
    private float maxHealth;
    [SerializeField] private float minAttackDamage;
    [SerializeField] private float maxAttackDamage;
    [SerializeField] private float minAttackSpeed;
    [SerializeField] private float maxAttackSpeed;
    [SerializeField] private float minMovementSpeed;
    [SerializeField] private float maxMovementSpeed;
    
    private Dictionary<Orbs, int> orbs = new Dictionary<Orbs, int>(){
        {Orbs.Red, 0},
        {Orbs.Yellow, 0},
        {Orbs.Purple, 0}
    };

    public delegate void StatsChange(float changeAmount);
    public static StatsChange ChangeAttackDamageEvent;
    public static StatsChange RestoreHealthEvent;
    public static StatsChange DamageEvent;
    public static StatsChange ChangeMovementSpeedEvent;
    public static StatsChange ChangeAttackSpeedEvent;
    public static StatsChange ChangeMaxHealthEvent;
 
    public delegate void OrbsChange(int red, int yellow, int purple);
    public static OrbsChange ChangeOrbsEvent;

    public delegate void SkillGain(Skill skill);
    public static SkillGain GainSkillEvent;
    
    public delegate void StatsReset();
    public static StatsReset ResetStatsEvent;

    private SpriteRenderer mySR;

    #endregion
    
    #region Methods
    
    // Start is called before the first frame update
    private void Start()
    {
        PlayerStats.DamageEvent += Damage;
        PlayerStats.ChangeAttackDamageEvent += ChangeAttackDamage;
        PlayerStats.RestoreHealthEvent += RestoreHealth;
        PlayerStats.ChangeAttackSpeedEvent += ChangeAttackSpeed;
        PlayerStats.ChangeMovementSpeedEvent += ChangeMovementSpeed;
        PlayerStats.ChangeOrbsEvent += ChangeOrbs;
        PlayerStats.GainSkillEvent += GainSkill;
        PlayerStats.ChangeMaxHealthEvent += ChangeMaxHealth;
        PlayerStats.ResetStatsEvent += ResetStats;
        ResetStats();
    }

    private void Awake()
    {
        this.mySR = GetComponent<SpriteRenderer>();
        this.skillsList.Add(Skill.Explosion);
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    private void Damage(float damageAmount)
    {
        this.health -= damageAmount;
        if (this.health < 0)
        {
            // some death animation
            Destroy(this.gameObject);
        }
        StartCoroutine("FlashRedOnDamage"); // or have a more elaborate animation
    }

    private IEnumerator FlashRedOnDamage() {
        mySR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.15f);
        mySR.color = new Color(1, 1, 1, 1);

    }

    public float GetMovementSpeed()
    {
        return this.movementSpeed;
    }

    private void ChangeMovementSpeed(float amount)
    {
        if (amount < 0)
        {
            this.movementSpeed = Mathf.Max(this.minMovementSpeed, this.movementSpeed - amount);
        } else
        {
            this.movementSpeed = Mathf.Min(this.maxMovementSpeed, this.movementSpeed + amount);
        }
    }

    private void ChangeAttackSpeed(float amount)
    {
        if (amount < 0)
        {
            this.attackSpeed = Mathf.Max(this.minAttackSpeed, this.attackSpeed - amount);
        } else 
        {
            this.attackSpeed = Mathf.Min(this.maxAttackSpeed, this.attackSpeed + amount);
        }
    }

    private void ChangeAttackDamage(float amount)
    {
        if (amount < 0)
        {
            this.attackDamage = Mathf.Max(this.minAttackDamage, this.attackDamage - amount);
        } else
        {
            this.attackDamage = Mathf.Min(this.maxAttackDamage, this.attackDamage + amount);
        }
    }

    private void RestoreHealth(float amountRestored)
    {
        this.health = Mathf.Min(this.maxHealth, this.health + amountRestored);
    }

    private void ChangeMaxHealth(float amount)
    {
        float ratio = this.health / this.maxHealth;
        this.maxHealth += amount;
        this.health = ratio * this.maxHealth;
    }

    private void ChangeOrbs(int red, int yellow, int purple)
    {
        // assuming that the value stored can never go below 0
        this.orbs[Orbs.Red] += red;
        this.orbs[Orbs.Yellow] += yellow;
        this.orbs[Orbs.Purple] += purple;
    }

    
    private void GainSkill(Skill skill)
    {
        this.skillsList.Add(skill);
    }
    
    public Skill RetrieveSkill(int index)
    {   
        return skillsList[index];   
    }

    public int GetNumberOfSkills()
    {
        return skillsList.Count;
    }

    private void ResetStats()
    {
        this.maxHealth = this.baseMaxHealth;
        this.attackDamage = this.baseAttackDamage;
        this.attackSpeed = this.baseAttackSpeed;
        this.movementSpeed = this.baseMovementSpeed;
        this.health = this.maxHealth;
    } 

    private void OnDestroy()
    {
        PlayerStats.DamageEvent -= Damage;
        PlayerStats.ChangeAttackDamageEvent -= ChangeAttackDamage;
        PlayerStats.RestoreHealthEvent -= RestoreHealth;
        PlayerStats.ChangeAttackSpeedEvent -= ChangeAttackSpeed;
        PlayerStats.ChangeMovementSpeedEvent -= ChangeMovementSpeed;
        PlayerStats.GainSkillEvent -= GainSkill;
        PlayerStats.ChangeMaxHealthEvent -= ChangeMaxHealth;
        PlayerStats.ResetStatsEvent -= ResetStats;
    }
    
    #endregion
}
