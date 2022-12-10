////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemyAI.cs
//FileType: Visual C# Source file
//Author : Woonhak Kong
//STU Number : 101300258
//Last Modified On : 12/10/2022
//Copy Rights : Gamdekong Studio
//Description : Class for Enemy AI
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
    private EnemyController self;
    public EnemyAI(EnemyController enemyController)
    {
        self = enemyController;

    }

    public void Initalize()
    {
        self.SetDirection(Vector2.left);
    }

    public void ExecuteAI()
    {
        if (self.GetIsCliif() || self.GetIsStickToWall())
        {
            TurnBack();
        }
        if (self.GetComponentInChildren<PlayerDetection>().GetLOS())
        {
            if (!self.GetComponentInChildren<PlayerDetection>().IsLookingAtPlayer())
            {
                TurnBack();
            }
        }
    }

    private void TurnBack()
    {
        if (self.GetDirection().x > 0.0f)
        {
            self.SetDirection(Vector2.left);
        }
        else if (self.GetDirection().x < 0.0f)
        {
            self.SetDirection(Vector2.right);
        }
    }
}
