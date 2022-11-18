////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MainSceneUIManager.cs
//FileType: Visual C# Source file
//Author : Woonhak Kong
//STU Number : 101300258
//Last Modified On : 11/18/2022
//Copy Rights : Gamdekong House
//Description : Class for controlling MainScene UI
////////////////////////////////////////////////////////////////////////////////////////////////////////



using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    public GameObject InstructionPannel;


    public void OnClickStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickInstruction()
    {
        InstructionPannel.SetActive(true);

    }

    public void OnClickBack()
    {
        InstructionPannel.SetActive(false);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

   
}
