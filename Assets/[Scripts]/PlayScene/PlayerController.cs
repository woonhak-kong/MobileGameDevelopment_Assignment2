using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;

public enum PlayerState
{
    IDLE,
    RUN,
    JUMP_UP,
    JUMP_DOWN,
    ATTACK,
    HIT,
}


public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpFactor;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isStickyToWall;
    [SerializeField] private bool firstJump;
    [SerializeField] private bool doubleJump;
    

    [SerializeField] private BoxCollider2D groundCheckBox;
    [SerializeField] private BoxCollider2D forwardCheckBox;

    [SerializeField] private PlayerState currentState;

    private Animator animator;


    public ContactFilter2D groundContactFilter;

    private Rigidbody2D rigidbody2D;

    private Vector2 characterDirection;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = PlayerState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsOnGround();
        UpdateIsStickyToWall();
        UpdatePlayerState();

        //SetAnimation();
    }

    private void UpdatePlayerState()
    {
        if (rigidbody2D.velocity.y > 0.0f && !isOnGround)
        {
            currentState = PlayerState.JUMP_UP;
        }
        else if(rigidbody2D.velocity.y < 0.0f && !isOnGround)
        {
            currentState = PlayerState.JUMP_DOWN;
        }
        else if(rigidbody2D.velocity.x != 0.0f)
        {
            currentState = PlayerState.RUN;
        }
        else if(rigidbody2D.velocity.x == 0.0f)
        {
            currentState = PlayerState.IDLE;
        }

        animator.SetInteger("factor", (int)currentState);
    }

    private void SetAnimation()
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        DoBehavior();
    }

    private void DoBehavior()
    {
        // Moving
        float velocityX = characterDirection.x * speed * Time.fixedDeltaTime;

        if (velocityX < 0.0f)
        {
            transform.localScale = new Vector2(-1.0f, transform.localScale.y);
        }
        else if(velocityX > 0.0f)
        {
            transform.localScale = new Vector2(1.0f, transform.localScale.y);
        }
     

        if (isStickyToWall)
        {
            rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(velocityX, rigidbody2D.velocity.y);
        }

    }

    private void UpdateIsOnGround()
    {
        List<Collider2D> collisions = new List<Collider2D>();
        int result = groundCheckBox.OverlapCollider(groundContactFilter, collisions);
        isOnGround = result > 0;
        if (isOnGround && rigidbody2D.velocity.y <= 0.5f)
        {
            firstJump = false;
            doubleJump = false;
        }
    }

    private void UpdateIsStickyToWall()
    {
        List<Collider2D> collisions = new List<Collider2D>();
        int result = forwardCheckBox.OverlapCollider(groundContactFilter, collisions);
        isStickyToWall = result > 0;
       
    }



    // input system
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        characterDirection = context.ReadValue<Vector2>();
    }

    // input system
    public void Jump(InputAction.CallbackContext context)
    {
        // only pressed
        if (context.performed)
        {
            //if (isOnGround && !firstJump)
            //{
            //    Debug.Log("jump = " + context.ReadValueAsButton());
            //    firstJump = true;
            //    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0.0f);
            //    rigidbody2D.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);
            //}
            if (!firstJump)
            {
                firstJump = true;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0.0f);
                rigidbody2D.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);
            }
            else if (!doubleJump)
            {
                doubleJump = true;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0.0f);
                rigidbody2D.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);
            }
        }
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        // checking ground
        float3 groundCheckPosition = new float3(groundCheckBox.gameObject.transform.position.x + groundCheckBox.offset.x,
            groundCheckBox.gameObject.transform.position.y + groundCheckBox.offset.y, 0.0f);
        Gizmos.DrawWireCube(groundCheckPosition, groundCheckBox.size);

        // checking forward
        float3 forwardCheckPosition = new float3(forwardCheckBox.gameObject.transform.position.x + forwardCheckBox.offset.x,
            forwardCheckBox.gameObject.transform.position.y + forwardCheckBox.offset.y, 0.0f);
        Gizmos.DrawWireCube(forwardCheckPosition, forwardCheckBox.size);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
        }
    }


}
