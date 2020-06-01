using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CG.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] int ammo = 10;
        [SerializeField] float attackRange = 50f;
        [SerializeField] float targetingRange = 100f;
        [SerializeField] float minRange = 0f;
        [SerializeField] Transform objectToPan = null;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] AudioClip shootSFX = null;
        [SerializeField] ParticleSystem bulletsParticleSystem = null;
        [SerializeField] float damping = 1f;
        [Range(0f,1f)][SerializeField] float shootSFXVolume = 0.5f;


        int ammoAtStart = 0;
        Transform target = null;
        float attackTimer = 0f;
        Type targetType;
        bool isTargetInRange = false;

        TargetFinder targetFinder;
        ObjectManager objectManager;

        private void Start()
        {
            ammoAtStart = ammo;
            targetFinder = ScriptableObject.CreateInstance<TargetFinder>();
            objectManager = FindObjectOfType<ObjectManager>();
            targetType = targetFinder.GetTargetType(gameObject);
        }

        private void Update()
        {
            if (target == null || !isTargetInRange)
            {
                var targets = objectManager.GetTypesOf(targetType);
                target = targetFinder.GetClosestTarget(targets, transform);
            }
            
            if (!target) { return; }

            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget < targetingRange && distanceToTarget > minRange)
            {
                isTargetInRange = true;
            }
            if (isTargetInRange && (distanceToTarget > targetingRange || distanceToTarget < minRange))
            {
                target = null;
                return;
            }

            FollowTarget();

            if (CanShoot())
            {
                ProcessAttack();
            }
        }

        private void FollowTarget()
        {
            PlaceableObject placeable;
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget > targetingRange)
            {
                return;
            }
            if (TryGetComponent<PlaceableObject>(out placeable))
            {
                if (placeable.actionsEnabled)
                {
                    var rotation = Quaternion.LookRotation(target.position - objectToPan.transform.position);
                    objectToPan.transform.rotation = Quaternion.Slerp(objectToPan.transform.rotation, rotation, Time.deltaTime * damping);
                    return;
                }
            }
            else
            {
                var rotation = Quaternion.LookRotation(target.position - objectToPan.transform.position);
                objectToPan.transform.rotation = Quaternion.Slerp(objectToPan.transform.rotation, rotation, Time.deltaTime * damping);
            }
            
        }

        private bool CanShoot()
        {
            PlaceableObject placeable;
            if (TryGetComponent<PlaceableObject>(out placeable))
            {
                if (!placeable.actionsEnabled)
                {
                    return false;
                }
            }

            attackTimer += Time.deltaTime;
            
            if (attackTimer > timeBetweenAttacks)
            {
                attackTimer = 0f;
                return true;
            }
            else { return false; }
        }

        private void ProcessAttack()
        {
            float distanceToEnemy = Vector3.Distance(target.transform.position, gameObject.transform.position);
            if (distanceToEnemy < attackRange && ammo > 0)
            {
                ammo--;
                Shoot();
            }

            if (ammo <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void Shoot()
        {
            var 
                System = GetComponentInChildren<ParticleSystem>();
            if (bulletsParticleSystem == null)
            {
                Debug.LogWarning(gameObject.name + " does not have a ParticleSystem");
                return;
            }
            bulletsParticleSystem.Play();
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
        }

        public void RefillAmmo()
        {
            ammo = ammoAtStart;
        }
    }

}
