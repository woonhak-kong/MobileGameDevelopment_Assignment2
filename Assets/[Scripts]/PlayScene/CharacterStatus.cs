using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UIElements;

public class CharacterStatus : MonoBehaviour
{

    public GameObject HPObject;

    public SpriteRenderer hpbar;

    public float hp;
    public float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        //hpbar = HPObject.transform.GetComponentInChildren<SpriteRenderer>();
        SetHP(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 0.0f)
        {
            HPObject.transform.localScale = new Vector2(-2.0f, HPObject.transform.localScale.y);
        }
        else
        {
            HPObject.transform.localScale = new Vector2(2.0f, HPObject.transform.localScale.y);
        }
    }

    private void SetHP(float val)
    {
        hp = val;
        if (hp > maxHp)
            hp = maxHp;

        float barFactor = hp / maxHp * 2.0f;
        hpbar.transform.localScale = new Vector2(barFactor, 1.0f);
    }

    public void Damage(float val)
    {
        GetComponent<CharacterController>().Hit();
        hp -= val;
        if (hp <= 0.0f)
        {
            hp = 0.0f;
            GetComponent<CharacterController>().Dead();
        }
        SetHP(hp);
    }
}
