using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : CharacterController
{
    private EnemyAI AI;

    
    private void Awake()
    {
        AI = new EnemyAI(this);
        AI.Initalize();
    }

    protected override void ExecuteAI()
    {
        AI.ExecuteAI();
    }


}
