using CG.Combat;
using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.Player
{
    public class HomeBase : MonoBehaviour
    {
        float pointsMultiplier = 1;
        ObjectManager objectManager;
        public bool isHomebase = false;

        Score score = null;

        private void Start()
        {
            objectManager = FindObjectOfType<ObjectManager>();
            score = FindObjectOfType<Score>();
        }

        private void Update()
        {
            if (!GetComponent<PlaceableObject>().actionsEnabled)
            {
                GetComponent<Collider>().enabled = false;
                return;
            }

            score.IncreasePoints(Time.deltaTime * pointsMultiplier);

            if (isHomebase)
            {
                float health = GetComponent<Health>().GetHealth();
                score.SetHomebaseHealth(health);
            }

        }

        private void OnDestroy()
        {
            if (isHomebase)
            {
                score.SetHomebaseHealth(0f);
                LevelManager.Instance.SetState(State.End);
            }

            try
            {
                objectManager.placeableObjects.Remove(transform);
                objectManager.factories.Remove(GetComponent<PlaceableObject>());
            }
            catch (Exception)
            {

                //throw;
            }
        }   
    }
}
