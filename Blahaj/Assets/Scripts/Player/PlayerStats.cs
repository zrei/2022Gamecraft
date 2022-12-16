using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PlayerStats : MonoBehaviour
{

    #region Fields

    private Dictionary<Skill, int> skills = new Dictionary<Skill, int>{
        {Skill.Explosion, 0},
        {Skill.Poison, 0},
        {Skill.Stun, 0},
        {Skill.Healing, 0}
    };
    private float health;
    [SerializeField] private bool invulnerable;
    [SerializeField] private float baseAttackDamage;
    private float attackDamage;
    [SerializeField] private float baseAttackDuration; 
    private float attackDuration;
    [SerializeField] private float baseAttackDashPower;
    private float attackDashPower;
    [SerializeField] private float baseAttackCooldown;
    private float attackCooldown;
    [SerializeField] private float baseAttackSpeed; 
    private float attackSpeed;
    [SerializeField] private float baseMovementSpeed;
    private float movementSpeed;
    [SerializeField] private float baseMaxHealth;
    private float maxHealth;
    [SerializeField] private float minAttackDamage;
    [SerializeField] private float maxAttackDamage;
    [SerializeField] private float minAttackDuration;
    [SerializeField] private float maxAttackDuration;
    [SerializeField] private float minAttackCooldown;
    [SerializeField] private float maxAttackCooldown;
    [SerializeField] private float minAttackDashPower;
    [SerializeField] private float maxAttackDashPower;
    [SerializeField] private float minAttackSpeed;
    [SerializeField] private float maxAttackSpeed;
    [SerializeField] private float minMovementSpeed;
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private Sprite stunSprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite explodeSprite;
    [SerializeField] private Sprite poisonSprite;

    [SerializeField] private float baseExplosionRadius;
    [SerializeField] private float basePoisonRadius;
    [SerializeField] private float baseStunRadius;
    [SerializeField] private float baseExplosionDamage;
    [SerializeField] private float baseStunDamage;
    [SerializeField] private float basePoisonDamage;
    [SerializeField] private float basePoisonTime;
    [SerializeField] private float baseStunTime;
    [SerializeField] private float baseExplosionSpeed;
    [SerializeField] private float baseHealAmount;
    [SerializeField] private float baseExplosionCooldown;
    [SerializeField] private float baseStunCooldown;
    [SerializeField] private float basePoisonCooldown;
    [SerializeField] private float baseHealCooldown;
    
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
    public static StatsChange ChangeAttackDurationEvent;
    public static StatsChange ChangeAttackCooldownEvent;
 
    public delegate void OrbsChange(int red, int yellow, int purple);
    public static OrbsChange ChangeOrbsEvent;

    public delegate void SkillGain(Tuple<Skill, int> skill);
    public static SkillGain GainSkillEvent;
    
    public delegate void StatsReset();
    public static StatsReset ResetStatsEvent;

    #endregion
    
    #region Methods
    
    // Start is called before the first frame update
    private void Start()
    {
        PlayerStats.DamageEvent += Damage;
        PlayerStats.ChangeAttackDamageEvent += ChangeAttackDamage;
        PlayerStats.RestoreHealthEvent += RestoreHealth;
        //PlayerStats.ChangeAttackSpeedEvent += ChangeAttackSpeed;
        PlayerStats.ChangeAttackDurationEvent += ChangeAttackDuration;
        PlayerStats.ChangeAttackCooldownEvent += ChangeAttackCooldown;
        PlayerStats.ChangeMovementSpeedEvent += ChangeMovementSpeed;
        PlayerStats.ChangeOrbsEvent += ChangeOrbs;
        PlayerStats.GainSkillEvent += GainSkill;
        PlayerStats.ChangeMaxHealthEvent += ChangeMaxHealth;
        PlayerStats.ResetStatsEvent += ResetStats;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.GetStateEvent() == GameState.NewGame)
        {
            GameManager.ChangeStateEvent(GameState.InGame);
        }
        /* Debug health bar
        if (Input.GetKeyDown(KeyCode.Space)) {
            Damage(2f);
        }
        */
    }
    
    public void Damage(float damageAmount)
    {
        if (!this.invulnerable)
        {
            this.health -= damageAmount;
            HealthBar.SetHealthEvent(this.health);
            if (this.health <= 0)
            {
                // some death animation
                Destroy(GameObject.FindWithTag("Player"));
                GameManager.ChangeStateEvent(GameState.LoseGame);
            }
            SpriteChanger.FlashRedEvent();
        }
    }

    public float GetMovementSpeed()
    {
        return this.movementSpeed;
    }

    public void ChangeMovementSpeed(float amount)
    {
        if (amount < 0)
        {
            this.movementSpeed = Mathf.Max(this.minMovementSpeed, this.movementSpeed - amount);
        } else
        {
            this.movementSpeed = Mathf.Min(this.maxMovementSpeed, this.movementSpeed + amount);
            //Debug.Log("Movement speed is "  + this.movementSpeed);
        }
    }

    public void ChangeMovementSpeedNoLimit(float amount)
    {
        this.movementSpeed = this.movementSpeed + amount;
    }

    public float GetAttackDuration()
    {
        return this.attackDuration;
    }
    private void ChangeAttackDuration(float amount)
    {
        if (amount < 0)
        {
            this.attackDuration = Mathf.Max(this.minAttackDuration, this.attackDuration - amount);
        } else 
        {
            this.attackDuration = Mathf.Min(this.maxAttackDuration, this.attackDuration + amount);
        }
    }

    public float GetAttackCooldown()
    {
        return this.attackCooldown;
    }
    private void ChangeAttackCooldown(float amount)
    {
        if (amount < 0)
        {
            this.attackCooldown = Mathf.Max(this.minAttackCooldown, this.attackCooldown - amount);
        } else 
        {
            this.attackCooldown = Mathf.Min(this.maxAttackCooldown, this.attackCooldown + amount);
        }
    }
    public float GetAttackDamage()
    {
        return this.attackDamage;
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

    public float GetAttackDashPower()
    {
        return this.attackDashPower;
    }
    private void ChangeAttackDashPower(float amount)
    {
        if (amount < 0)
        {
            this.attackDashPower = Mathf.Max(this.minAttackDashPower, this.attackDashPower - amount);
        } else
        {
            this.attackDashPower = Mathf.Min(this.maxAttackDashPower, this.attackDashPower + amount);
        }
    }

    public bool GetInvulnerable() 
    {
        return this.invulnerable;
    }

    public void SetInvulnerable(bool newInvul) 
    {
        this.invulnerable = newInvul;
    }

    private void RestoreHealth(float amountRestored)
    {
        //Debug.Log("Heal amount " + amountRestored);
        this.health = Mathf.Min(this.maxHealth, this.health + amountRestored);
        //Debug.Log("Health now " + this.health);
        HealthBar.SetHealthEvent(this.health);
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
    
    private void GainSkill(Tuple<Skill, int> skill)
    {

        switch (skill.Item1)
        {
            case Skill.Explosion:
                this.skills[skill.Item1] += skill.Item2;
                break;
            case Skill.Stun:
                this.skills[skill.Item1] += skill.Item2;
                break;
            case Skill.Poison:
                this.skills[skill.Item1] += skill.Item2;
                break;
            case Skill.Healing:
                this.skills[skill.Item1] += skill.Item2;
                break;
            case Skill.HpUp:
                ChangeMaxHealth(skill.Item2 * 0.5f);
                break;
            case Skill.AttackSpeedUp:
                ChangeAttackCooldown(skill.Item2 - 0.5f);
                break;
            case Skill.AttackUp:
                ChangeAttackDamage(skill.Item2 * 0.5f);
                break;
            case Skill.MovementSpeedUp:
                ChangeMovementSpeed(skill.Item2 * 0.5f);
                break;
        }
        //this.skills[skill.Item1] += skill.Item2;

    }
    
    public int RetrieveSkillLevel(Skill skill)
    {   
        //Debug.Log(skills);
        return skills[skill];   
    }

    public Tuple<float, float, float> GetSkillInfo(Skill skill)
    {
        int skillLevel = RetrieveSkillLevel(skill);
        if (skill == Skill.Explosion)
        {
            //scale the info based on the skill level somehow
            return new Tuple<float, float, float>(this.baseExplosionDamage + 0.5f * (skillLevel - 1), this.baseExplosionRadius + 0.2f * (skillLevel - 1), this.baseExplosionSpeed + 0.1f * (skillLevel - 1));
        } else if (skill == Skill.Poison)
        {
            return new Tuple<float, float, float>(this.basePoisonDamage + 0.5f * (skillLevel - 1), this.basePoisonRadius + 0.2f * (skillLevel - 1), this.basePoisonTime + 0.2f * (skillLevel - 1));
        } else if (skill == Skill.Stun)
        {
            return new Tuple<float, float, float>(this.baseStunDamage + 0.5f * (skillLevel - 1), this.baseStunRadius + 0.2f * (skillLevel - 1), this.baseStunTime + 0.2f * (skillLevel - 1));
        } else if (skill == Skill.Healing) 
        {
            return new Tuple<float, float, float>(this.baseHealAmount + 0.2f * (skillLevel - 1), 0f, 0f);
        } else 
        {
            return new Tuple<float, float, float>(0f, 0f, 0f);
        }
    }

    public float GetCooldown(Skill skill)
    {
        int skillLevel = RetrieveSkillLevel(skill);
        //use to factor cooldown
        if (skill == Skill.Stun) 
        {
            return this.baseStunCooldown - 0.1f * (skillLevel - 1);    
        } else if (skill == Skill.Poison)
        {
            return this.basePoisonCooldown - 0.1f * (skillLevel - 1);
        } else if (skill == Skill.Explosion)
        {
            return this.baseExplosionCooldown - 0.1f * (skillLevel - 1);
        } else if (skill == Skill.Healing)
        {
            return this.baseHealCooldown - 0.1f * (skillLevel - 1);
        } else 
        {
            return 0f;
        }
        
    }
    private void ResetStats()
    {
        this.maxHealth = this.baseMaxHealth;
        this.attackDamage = this.baseAttackDamage;
        this.attackDuration = this.baseAttackDuration;
        this.attackCooldown = this.baseAttackCooldown;
        this.attackSpeed = this.baseAttackSpeed;
        this.movementSpeed = this.baseMovementSpeed;
        this.health = this.maxHealth;
        ResetOrbs();
        ResetSkills();
        ChangeSprite(SpriteStates.Normal);
    }

    private void ResetOrbs()
    {
        orbs[Orbs.Purple] = 0;
        orbs[Orbs.Red] = 0;
        orbs[Orbs.Yellow] = 0;
    }

    private void ResetSkills()
    {
        skills[Skill.Explosion] = 0;
        skills[Skill.Stun] = 0;
        skills[Skill.Poison] = 0;
        skills[Skill.Healing] = 0;
    }

    public void ChangeSprite(SpriteStates state)
    {
        switch (state)
        {
            case (SpriteStates.Normal):
                SpriteChanger.SpriteChangeEvent(normalSprite);
                break;
            case (SpriteStates.Stun):
                SpriteChanger.SpriteChangeEvent(stunSprite);
                break;
            case (SpriteStates.Poison):
                SpriteChanger.SpriteChangeEvent(poisonSprite);
                break;
            case (SpriteStates.Explosion):
                SpriteChanger.SpriteChangeEvent(explodeSprite);
                break;
        }
    }

    private void OnDestroy()
    {
        PlayerStats.DamageEvent -= Damage;
        PlayerStats.ChangeAttackDamageEvent -= ChangeAttackDamage;
        PlayerStats.RestoreHealthEvent -= RestoreHealth;
        PlayerStats.ChangeAttackDurationEvent -= ChangeAttackDuration;
        PlayerStats.ChangeAttackCooldownEvent -= ChangeAttackCooldown;
        //PlayerStats.ChangeAttackSpeedEvent -= ChangeAttackSpeed;
        PlayerStats.ChangeMovementSpeedEvent -= ChangeMovementSpeed;
        PlayerStats.GainSkillEvent -= GainSkill;
        PlayerStats.ChangeMaxHealthEvent -= ChangeMaxHealth;
        PlayerStats.ResetStatsEvent -= ResetStats;
    }

    public int getRedOrbs() {
        return this.orbs[Orbs.Red];
    }

    public int getYellowOrbs() {
        return this.orbs[Orbs.Yellow];
    }

    public int getPurpleOrbs() {
        return this.orbs[Orbs.Purple];
    }

    public float getHealth()
    {
        return this.health;
    }

    public float getMaxHealth()
    {
        return this.maxHealth;
    }
    
    #endregion
}
