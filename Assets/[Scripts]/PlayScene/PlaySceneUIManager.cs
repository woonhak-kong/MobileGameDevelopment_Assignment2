////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlaySceneUIManager.cs
//FileType: Visual C# Source file
//Author : Woonhak Kong
//STU Number : 101300258
//Last Modified On : 11/18/2022
//Copy Rights : Gamdekong House
//Description : Class for controlling PlayScene UI
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUIManager : MonoBehaviour
{
    public GameObject GameoverPannel;
    public TMPro.TextMeshProUGUI score;
    public Slider HPBar;

    public void OnClickGotoMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EnableGameoverPannel()
    {
        GameoverPannel.SetActive(true);
    }

    public void SetScoreText(int value)
    {
        score.text = "Score : " + value.ToString();
    }

    public void SetHPBar(float val)
    {
        HPBar.value = val;
    }

}
