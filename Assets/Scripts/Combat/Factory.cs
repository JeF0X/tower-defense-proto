using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CG.Player
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] int maxWorkers = 10;
        [SerializeField] float timeBetweenSmokesMultiplier = 1f;
        [SerializeField] Material hoverMaterial = null;
        [SerializeField] ParticleSystem smokeParticleSystem = null;
        [SerializeField] float pointsPerSecond = 1;


        Material startMaterial;
        float pointsPerClick = 1;
        int workers = 1;
        float workerSmokeTimer = 0;
        ObjectManager objectManager;
        Score score = null;

        private void Awake()
        {
            objectManager = FindObjectOfType<ObjectManager>();
        }

        private void Start()
        {
            startMaterial = GetComponentInChildren<MeshRenderer>().material;
            score = FindObjectOfType<Score>();
        }

        private void Update()
        {
            if (!GetComponent<PlaceableObject>().actionsEnabled)
            {
                GetComponent<Collider>().enabled = false;
                return;
            }

            if (LevelManager.Instance.GetState() == State.Game)
            {
                score.IncreasePoints(Time.deltaTime * pointsPerSecond);
            }

            if (workers > 0)
            {
                WorkerSmoke();
            }

        }

        private void OnDestroy()
        {
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

        private void WorkerSmoke()
        {
            workerSmokeTimer += Time.deltaTime;
            float timeBetweenSmokes = maxWorkers / workers * timeBetweenSmokesMultiplier;
            if (timeBetweenSmokes < workerSmokeTimer)
            {
                workerSmokeTimer = 0f;
                PlaySmokeEffect();
            }
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                score.IncreasePoints(pointsPerClick * pointsPerSecond + objectManager.placeableObjects.Count);
                PlaySmokeEffect();
            }
        }

        private void PlaySmokeEffect()
        {
            smokeParticleSystem.Play();
        }
    }

}