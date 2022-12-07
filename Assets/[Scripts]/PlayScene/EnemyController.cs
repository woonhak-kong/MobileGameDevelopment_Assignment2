using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    private EnemyAI AI;

    
    private void Awake()
    {
        AI = new EnemyAI(this);
        AI.Initalize();
    }

    protected override void ExecuteAI()
    {
        AI.ExecuteAI();
    }

    public override void Hit()
    {
        base.Hit();
        rigidbody2D.velocity = Vector3.zero;
    }

    public override void Dead()
    {
        base.Dead();
        GameObject coin = FindObjectOfType<LevelManager>().CoinPrefabs;
        int random = UnityEngine.Random.Range(1, 10);
        for (int i = 0; i < random; i++)
        {
            GameObject tmp = Instantiate(coin);
            tmp.transform.position = transform.position;
            tmp.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10.0f, ForceMode2D.Impulse);
        }
    }

    public void Damage(float damage)
    {
        GetComponent<CharacterStatus>().Damage(damage);
    }

}
