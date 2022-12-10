////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerAttacher.cs
//FileType: Visual C# Source file
//Author : Woonhak Kong
//STU Number : 101300258
//Last Modified On : 12/10/2022
//Copy Rights : Gamdekong Studio
//Description : Class for attaching player to platform
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacher : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Enter  " +other.gameObject.name);
        other.gameObject.transform.SetParent(transform);

    }

    public void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Exit  " + other.gameObject.name);
        other.gameObject.transform.SetParent(null);

    }

}
