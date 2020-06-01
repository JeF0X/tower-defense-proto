using CG.Combat;
using CG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.AI
{
    public class Begin : AIState
    {
        public Begin(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override IEnumerator Start()
        {
            EnemyAI.SetAIState(new FindTarget(EnemyAI));
            yield break;
        }

    }
}