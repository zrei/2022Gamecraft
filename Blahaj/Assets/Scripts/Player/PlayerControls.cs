using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Constants;
public class PlayerControls : MonoBehaviour
{

    #region fields

    [SerializeField] private float collisionOffset = 0.05f;
    
    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;
    private Vector2 dashdir;
    private bool attacking = false;
    private bool canAttack = true;
    private bool canPoison = true;
    private bool canStun = true;
    private bool canExplode = true;
    private bool canHeal = true;
    private Vector2 movementInput;
    [SerializeField] private ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private PlayerStats stats;
    private SkillsControl skills;

    #endregion

    private void Awake()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        this.mySR = GetComponent<SpriteRenderer>();
        this.stats = GameObject.FindWithTag("GameController").GetComponent<PlayerStats>();
        this.skills = GetComponent<SkillsControl>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.castCollisions = new List<RaycastHit2D>();
    }

    private void OnMove(InputValue movementValue) 
    {
        this.movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        this.attacking = true;
    }

    private void FixedUpdate()
    {
        if (this.movementInput != Vector2.zero && attacking!=true)
        {
            
            int numCollisions = rb2D.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                stats.GetMovementSpeed() * Time.fixedDeltaTime + collisionOffset
                );

            
            if (numCollisions == 0)
            {
                    rb2D.MovePosition(rb2D.position + movementInput * stats.GetMovementSpeed() * Time.fixedDeltaTime);
            }
            

            //float playerSpeed  =  stats.GetMovementSpeed();

            //rb2D.velocity  = movementInput * playerSpeed;

            //rb2D.MovePosition(rb2D.position + movementInput * stats.GetMovementSpeed() * Time.fixedDeltaTime);
            
            //Debug.Log(movementInput);
            Quaternion rotationZ = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*Mathf.Atan(this.movementInput.y / this.movementInput.x));                
            //Debug.Log(rotationZ);
            transform.rotation = rotationZ;

            float x = Mathf.Abs(transform.localScale.x);
            float y = transform.localScale.y;
            float z = transform.localScale.z;
            if (movementInput.x >= 0) {
                transform.localScale = new Vector3(x, y, z);
                //mySR.flipX = true;
            } else if (movementInput.x < 0) {
                transform.localScale = new Vector3(-x, y, z);
                //mySR.flipX = false;
            }

        }        

        //}
        /*if (this.attacking)
        {
            // Attack! With cooldown
            //Debug.Log("Attack");
            this.attacking = false;
        }*/
    }

    private void Update()
    {
        dashdir = new Vector2(movementInput.x, movementInput.y).normalized;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // attack
            Debug.Log("Space has been pressed");
            if (canAttack) 
            {
                Debug.Log("Attack able, attacking");
                StartCoroutine(Attack());
            }
        }
        if (GameManager.GetStateEvent() != GameState.Crafting)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (canHeal && stats.RetrieveSkillLevel(Skill.Healing) > 0)
                {
                    Debug.Log("Healing?");
                    //Debug.Log("Have Skill, Cooldown okay");
                    skills.UseSkill(Skill.Healing);
                    StartCoroutine(SkillCooldown(Skill.Healing, stats.GetCooldown(Skill.Healing)));
                }
                // check skill 4
                //Debug.Log("O has been pressed");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // explosion
            // check skill 1
            if (canExplode && stats.RetrieveSkillLevel(Skill.Explosion) > 0)
            {
                //Debug.Log("Have Skill, Cooldown okay");
                skills.UseSkill(Skill.Explosion);
                StartCoroutine(SkillCooldown(Skill.Explosion, stats.GetCooldown(Skill.Explosion)));
            }
            //Debug.Log("Y has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (canStun && stats.RetrieveSkillLevel(Skill.Stun) > 0)
            {
            // Debug.Log("Have Skill, Cooldown okay");
                skills.UseSkill(Skill.Stun);
                StartCoroutine(SkillCooldown(Skill.Stun, stats.GetCooldown(Skill.Stun)));
            }
            // check skill 2
            //Debug.Log("U has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (canPoison && stats.RetrieveSkillLevel(Skill.Poison) > 0)
            {
                //Debug.Log("Have Skill, Cooldown okay");
                skills.UseSkill(Skill.Poison);
                StartCoroutine(SkillCooldown(Skill.Poison, stats.GetCooldown(Skill.Poison)));
            }
            // check skill 3
            //Debug.Log("I has been pressed");
        }
        
    }

    private IEnumerator SkillCooldown(Skill skill, float cooldown)
    {
        float time = 0f;
        float interval = 0.3f;
        switch (skill)
        {
            case (Skill.Explosion):
                this.canExplode = false;
                break;
            case (Skill.Poison):
                this.canPoison = false;
                break;
            case (Skill.Stun):
                this.canStun = false;
                break;
            case (Skill.Healing):
                this.canHeal = false;
                break;
        }
        while (time < cooldown) 
        {
            yield return new WaitForSeconds(interval);
            time += interval;
        }
        switch (skill)
        {
            case (Skill.Explosion):
                this.canExplode = true;
                break;
            case (Skill.Poison):
                this.canPoison = true;
                break;
            case (Skill.Stun):
                this.canStun = true;
                break;
            case (Skill.Healing):
                this.canHeal = true;
                break;
        }
    }

    private IEnumerator Attack() {
        Debug.Log("Attack has been triggered");
        float dashPower = 150;  
        this.attacking = true;
        this.canAttack = false;
        //float dashPower = stats.GetAttackDashPower();
        rb2D.velocity = dashdir * dashPower;
        stats.SetInvulnerable(true);
        yield return new WaitForSeconds(stats.GetAttackDuration());
        this.attacking = false;
        rb2D.velocity = new Vector2(0, 0);
        //stats.ChangeMovementSpeedNoLimit(-1 * dashPower);
        stats.SetInvulnerable(false);
        yield return new WaitForSeconds(stats.GetAttackCooldown());
        this.canAttack = true;
    }

    public bool getAttacking() {
        return attacking;
    }

}
