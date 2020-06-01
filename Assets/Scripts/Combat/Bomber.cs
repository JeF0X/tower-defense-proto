using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.AI;

namespace CG.Combat
{
    public class Bomber : MonoBehaviour
    {
        [SerializeField] float attackDistance = 10f;

        Transform target = null;
        EnemyAI enemyAI = null;

        private void Start()
        {
            enemyAI = GetComponent<EnemyAI>();
        }

        private void Update()
        {
            try
            {
                target = enemyAI.target.transform;
                ProcessAttacking();
            }
            catch (System.Exception)
            {
                
            }
            
        }

        private void ProcessAttacking()
        {
            
            if (target == null)
            {
                return;
            }
            if (Vector3.Distance(transform.position, target.position) < attackDistance)
            {
                Health targetHealth;
                if (target.TryGetComponent<Health>(out targetHealth))
                {
                    targetHealth.ProcessHit(GetComponent<Health>().GetDamage());
                    GetComponent<Health>().Die();
                    return;
                }
                else { Destroy(gameObject); }
            }
        }
    }
}
