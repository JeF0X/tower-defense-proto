using CG.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavToTarget : AIState
{
    public NavToTarget(EnemyAI enemyAI) : base(enemyAI)
    {
    }

    public override IEnumerator Start()
    {
        if (EnemyAI.target == null)
        {
            EnemyAI.SetAIState(new FindTarget(EnemyAI));
            yield break;
        }
        EnemyAI.navMeshAgent.destination = EnemyAI.target.transform.position;
        yield break;
    }

    public override void Tick()
    {
        if (EnemyAI.target == null)
        {
            EnemyAI.SetAIState(new FindTarget(EnemyAI));
        }
    }
}