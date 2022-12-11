using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    #region fields

    [SerializeField] private float movementSpeed;
    [SerializeField] private float collisionOffset = 0.05f;
    
    private Rigidbody2D rb2D;
    private SpriteRenderer mySR;

    private Vector2 movementInput;
    [SerializeField] private ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    #endregion

    private void Awake()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        this.mySR = GetComponent<SpriteRenderer>();
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

    private void FixedUpdate()
    {
        if (this.movementInput != Vector2.zero)
        {
            int numCollisions = rb2D.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                movementSpeed * Time.fixedDeltaTime + collisionOffset
                );

            if (numCollisions == 0)
            {
                rb2D.MovePosition(rb2D.position + movementInput * movementSpeed * Time.fixedDeltaTime);
            }
            //Debug.Log(movementInput);
            Quaternion rotationZ = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*Mathf.Atan(this.movementInput.y / this.movementInput.x));
            //Debug.Log(rotationZ);
            transform.rotation = rotationZ;
            
            //check direction
            if (movementInput.x > 0) {
                mySR.flipX = false;
            } else if (movementInput.x < 0) {
                mySR.flipX = true;
            }
        }
    }
}
