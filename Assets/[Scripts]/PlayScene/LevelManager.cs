using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public PlaySceneUIManager PlaySceneUIManager;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        this.score += score;
        PlaySceneUIManager.SetScoreText(score);
    }
}
