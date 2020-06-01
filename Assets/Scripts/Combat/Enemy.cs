using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.Core;
using System;

namespace CG.Combat
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] float pointsForKill = 10f;

        ObjectManager objectManager;

        private void Awake()
        {
            objectManager = FindObjectOfType<ObjectManager>();
            objectManager.enemies.Add(transform);

        }

        private void Update()
        {
            if (LevelManager.Instance.GetState() == State.End)
            {
                Destroy(gameObject);
            }
        }
        private void OnDestroy()
        {

            try
            {
                objectManager.enemies.Remove(transform);
            }
            catch (System.Exception)
            {

                Debug.LogWarning(name + " could not be removed from object list");
            }
        }

        internal float GetPoints()
        {
            return pointsForKill;
        }
    }
}
