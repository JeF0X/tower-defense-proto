using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.AI
{
    public abstract class AIState
    {

        protected EnemyAI EnemyAI;

        public AIState(EnemyAI enemyAI)
        {
            EnemyAI = enemyAI;
        }

        public virtual void Tick()
        {
            return;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator FindTarget()
        {
            yield break;
        }

        public virtual IEnumerator Move()
        {
            yield break;
        }

        public virtual IEnumerator Attack()
        {
            yield break;
        }
    }
}
