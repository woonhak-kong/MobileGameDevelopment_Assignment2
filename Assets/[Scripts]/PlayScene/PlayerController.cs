using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{
    public ContactFilter2D attackContactFilter;



    public void OnClickAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    // input system
    public void OnClickMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        SetDirection(context.ReadValue<Vector2>());
    }

    // input system
    public void OnClickJump(InputAction.CallbackContext context)
    {
        // only pressed
        if (context.performed)
        {
            Jump();
        }
    }

    public override void AttackEventForHit()
    {
        attackBox.size = new Vector2(2.0f, 2.0f);
        attackBox.isTrigger = true;

        List<Collider2D> collisions = new List<Collider2D>();
        int result = attackBox.GetComponent<BoxCollider2D>().OverlapCollider(attackContactFilter, collisions);
        if (result > 0)
        {
            Debug.Log("collision with " + collisions[0].name);
        }
    }
}
