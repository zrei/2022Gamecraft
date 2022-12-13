using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    #region fields

    [SerializeField] private float collisionOffset = 0.05f;
    
    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;

    private bool attacking = false;
    private Vector2 movementInput;
    [SerializeField] private ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    PlayerStats stats;

    #endregion

    private void Awake()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        this.mySR = GetComponent<SpriteRenderer>();
        this.stats = GetComponent<PlayerStats>();
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
        if (this.movementInput != Vector2.zero)
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
            //Debug.Log(movementInput);
            Quaternion rotationZ = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*Mathf.Atan(this.movementInput.y / this.movementInput.x));                //Debug.Log(rotationZ);
            transform.rotation = rotationZ;
            
            if (movementInput.x >= 0) {
                mySR.flipX = true;
            } else if (movementInput.x < 0) {
                mySR.flipX = false;
            }
            
            
        }
        if (this.attacking)
        {
            // Attack! With cooldown
            //Debug.Log("Attack");
            this.attacking = false;
        }
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // check skill 1
            Debug.Log("Y has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            // check skill 2
            Debug.Log("U has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // check skill 3
            Debug.Log("I has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            // check skill 4
            Debug.Log("O has been pressed");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            // check skill 5
            Debug.Log("P has been pressed");
        }
    }*/
}
