////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: LevelManager.cs
//FileType: Visual C# Source file
//Author : Woonhak Kong
//STU Number : 101300258
//Last Modified On : 12/10/2022
//Copy Rights : Gamdekong Studio
//Description : Class for setting level
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public PlaySceneUIManager PlaySceneUIManager;

    public GameObject CoinPrefabs;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale= 1.0f;
        SoundManager.Instance.PlayBgm("PlayBgm", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        this.score += score;
        PlaySceneUIManager.SetScoreText(this.score);
    }

    public void GameClear()
    {
        Time.timeScale = 0;
        PlaySceneUIManager.GameClear();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        PlaySceneUIManager.GameOver();
    }
}
