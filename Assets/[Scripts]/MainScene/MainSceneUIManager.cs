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

    private void Start()
    {
        Time.timeScale = 1.0f;
        SoundManager.Instance.PlayBgm("MainBgm");
    }

    public void OnClickStart()
    {
        SoundManager.Instance.PlayFX("Click");
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickInstruction()
    {
        InstructionPannel.SetActive(true);
        SoundManager.Instance.PlayFX("Click");
    }

    public void OnClickBack()
    {
        InstructionPannel.SetActive(false);
        SoundManager.Instance.PlayFX("Click");
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayFX("Click");
        Application.Quit();
    }

   
}
