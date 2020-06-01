using CG.Combat;
using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.Player;

namespace CG.AI
{
    public class FindTarget : AIState
    {
        public FindTarget(EnemyAI enemyAI) : base(enemyAI)
        {

        }

        public override IEnumerator Start()
        {
            PlaceableObject target = null;
            var targets = EnemyAI.ObjectManager.placeableObjects;
            var distanceToClosestTarget = Mathf.Infinity;

            foreach (var possibleTarget in targets)
            {
                if (possibleTarget.GetComponent<HomeBase>() != null && EnemyAI.attackHomebase)
                {
                    target = possibleTarget.GetComponent<PlaceableObject>();
                    break;
                }

                var distanceToPossibleTarget = Vector3.Distance(EnemyAI.transform.position, possibleTarget.position);
                if (distanceToPossibleTarget <= distanceToClosestTarget)
                {
                    target = possibleTarget.GetComponent<PlaceableObject>();
                    distanceToClosestTarget = distanceToPossibleTarget;
                }
            }
            EnemyAI.target = target;
            EnemyAI.SetAIState(new NavToTarget(EnemyAI));

            yield break;
        }
    }
}