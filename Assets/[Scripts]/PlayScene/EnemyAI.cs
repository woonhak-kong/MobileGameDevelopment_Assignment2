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
