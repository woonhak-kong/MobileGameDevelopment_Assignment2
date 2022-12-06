using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Enter  " +other.gameObject.name);
        other.gameObject.transform.SetParent(transform);
        if (other.gameObject.GetComponent<PlayerController>().GetIsGround())
        {
        }

    }

    public void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Exit  " + other.gameObject.name);
        other.gameObject.transform.SetParent(null);

    }

}
