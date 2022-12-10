////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerController.cs
//FileType: Visual C# Source file
//Author : Woonhak Kong
//STU Number : 101300258
//Last Modified On : 12/10/2022
//Copy Rights : Gamdekong Studio
//Description : Class for controlling player
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{
    public Joystick leftJoystick;

    public float power;

    private LevelManager levelManage;

    private void Start()
    {
        base.Start();
        leftJoystick = GameObject.FindObjectOfType<Joystick>();
        levelManage = FindObjectOfType<LevelManager>();
        levelManage.PlaySceneUIManager.SetHPBar(GetComponent<CharacterStatus>().GetHPRatio());
    }

    private void Update()
    {
        base.Update();
        if (!Application.isEditor)
        {
            float x = leftJoystick.Horizontal;

            if (Mathf.Abs(x) > 0.3f)
            {
                x = x > 0.0f ? 1.0f : -1.0f;
                SetDirection(new Vector2(x, 0.0f));
            }
            else
            {
                SetDirection(Vector2.zero);
            }
        }
        
    }

    public void OnClickAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerAttack();
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
            PlayerJump();
        }
    }

    public void PlayerAttack()
    {
        if (!isAttacking)
        {
            SoundManager.Instance.PlayFX("Slash", 0.1f);
        }
        Attack();
    }

    public void PlayerJump()
    {
        if (!firstJump || !doubleJump)
        {
            SoundManager.Instance.PlayFX("Jump");
        }
        Jump();
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
                    SoundManager.Instance.PlayFX("Hit");
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
            SoundManager.Instance.PlayFX("Hurt");
        }

        if (collision.gameObject.tag == "Portion")
        {
            Debug.Log("Portion!");
            GetComponent<CharacterStatus>().AddHp(50);
            Destroy(collision.gameObject);
            SoundManager.Instance.PlayFX("Cure");
        }

        if (collision.gameObject.tag == "Coin")
        {
            Debug.Log("Coin!");
            levelManage.AddScore(10);
            Destroy(collision.gameObject);
            SoundManager.Instance.PlayFX("PickupItem", 0.4f);
        }

        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("Goal!");
            levelManage.GameClear();
            SoundManager.Instance.PlayFX("Win");

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
        
        Invoke("GameOver", 0.5f);
        SoundManager.Instance.PlayFX("Dead");
    }

    public void GameOver()
    {
        SoundManager.Instance.PlayFX("Lose", 0.1f);
        levelManage.GameOver();
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

    public void FootSound()
    {
        SoundManager.Instance.PlayFX("BushStep", 0.5f);
    }
}
