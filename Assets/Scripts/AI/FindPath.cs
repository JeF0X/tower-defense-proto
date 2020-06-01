using CG.Combat;
using CG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.AI
{
    public class FindPath : AIState
    {
        public FindPath(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override IEnumerator Start()
        {
            if (EnemyAI.target == null)
            {
                EnemyAI.SetAIState(new FindTarget(EnemyAI));
                yield break;
            }
            var pathFinder = PathFinder.CreateInstance<PathFinder>();
            EnemyAI.path = pathFinder.GetPath(EnemyAI.currentWaypoint, EnemyAI.target.GetWaypoint());

            EnemyAI.SetAIState(new Move(EnemyAI));
        }

    }
}
