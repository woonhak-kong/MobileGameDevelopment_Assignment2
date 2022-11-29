using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpFactor;
    [SerializeField] private bool isJumping;

    private Rigidbody2D rigidbody2D;

    private Vector2 characterDirection;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float velocityX = characterDirection.x * speed * Time.fixedDeltaTime;
        rigidbody2D.velocity = new Vector2(velocityX, rigidbody2D.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        characterDirection = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // only pressed
        if (context.performed)
        {
            Debug.Log("jump = " + context.ReadValueAsButton());
            rigidbody2D.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);
        }
        
    }
}
