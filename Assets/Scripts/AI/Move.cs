using CG.Combat;
using CG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.AI
{

    public class Move : AIState
    {
        int waypointIndex = 0;
        Waypoint targetWaypoint = null;

        public Move(EnemyAI enemyAI) : base(enemyAI)
        {
        }

        public override void Tick()
        {
            if (EnemyAI.target == null)
            {
                EnemyAI.SetAIState(new FindTarget(EnemyAI));
            }

            if (waypointIndex >= EnemyAI.path.Count)
            {
                return;
            }

            targetWaypoint = EnemyAI.path[waypointIndex];
            Vector3 direction = targetWaypoint.transform.position - EnemyAI.transform.position;
            EnemyAI.transform.Translate(direction.normalized * EnemyAI.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(EnemyAI.transform.position, targetWaypoint.transform.position) <= 1f)
            {
                waypointIndex++;
                EnemyAI.currentWaypoint = EnemyAI.path[waypointIndex];
            }
        }
    }

}