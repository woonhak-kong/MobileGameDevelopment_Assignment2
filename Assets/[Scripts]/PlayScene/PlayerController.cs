using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{

    public float power;

    private LevelManager levelManage;

    private void Start()
    {
        base.Start();
        levelManage = FindObjectOfType<LevelManager>();
        levelManage.PlaySceneUIManager.SetHPBar(GetComponent<CharacterStatus>().GetHPRatio());
    }

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
        //Debug.Log(context.ReadValue<Vector2>());
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
            for (int i = 0; i < result; i++)
            {
                if (collisions[i].tag == "Enemy")
                {
                    Debug.Log("collision with " + collisions[i].name);
                    collisions[i].GetComponent<EnemyController>().Damage(power);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("damaged");
            GetComponent<CharacterStatus>().Damage(5);
            levelManage.PlaySceneUIManager.SetHPBar(GetComponent<CharacterStatus>().GetHPRatio());
        }

        if (collision.gameObject.tag == "Portion")
        {
            Debug.Log("Portion!");
            GetComponent<CharacterStatus>().AddHp(50);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coin")
        {
            Debug.Log("Coin!");
            levelManage.AddScore(10);
            Destroy(collision.gameObject);
        }

    }

    public override void Hit()
    {
        base.Hit();
        rigidbody2D.AddForce(new Vector2(transform.localScale.x * -1.0f * 10.0f, 10.0f), ForceMode2D.Impulse);
        
    }

    public override void Dead()
    {
        base.Dead();
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Floatingground"))
    //    {
    //        gameObject.transform.parent = null;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Ennter " + collision.gameObject.name);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("Exit " + collision.gameObject.name);
    //}
}
