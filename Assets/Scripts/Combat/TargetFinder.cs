using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.Combat
{

    public class TargetFinder : ScriptableObject
    {
        Transform seeker;
        public Type GetTargetType(GameObject seekerObject)
        {
            if (seekerObject.GetComponent<Enemy>())
            {
                return typeof(PlaceableObject);
            }
            else
            {
                return typeof(Enemy);
            }
        }

        public Transform GetClosestTarget(List<Transform> targets, Transform seeker)
        {
            this.seeker = seeker;

            if (targets.Count == 0)
            {
                return null;
            }

            Transform closestEnemy = targets[0].transform;

            foreach (Transform enemyToCompare in targets)
            {
                closestEnemy = FindClosest(closestEnemy, enemyToCompare.transform);
            }

            return closestEnemy;
        }

        private Transform FindClosest(Transform prevClosestEnemy, Transform enemyToCompare)
        {
            var distToPrevTarget = Vector3.Distance(prevClosestEnemy.position, seeker.position);
            var distToNewTarget = Vector3.Distance(enemyToCompare.transform.position, seeker.position);
            if (distToPrevTarget < distToNewTarget)
            {
                return prevClosestEnemy;
            }
            return enemyToCompare;
        }
    }
}