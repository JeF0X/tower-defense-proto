using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.Core;
using System;

namespace CG.Combat
{

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] Waypoint startWaypoint = null;
        [SerializeField] float secondsToSpawnAfter = 10f;
        [SerializeField] public Wave[] waves;
        bool isSpawning = true;
        int currentWaveIndex = 0;
        bool hasStartedSpawning = false;

        ObjectManager objectManager;

        [Serializable] public class Wave
        {
            public Transform altSpawnPoint = null;
            public int numberOfEnemies = 10;
            [Tooltip("Enemies per second")] public float timeBetweenSpawns = 1f;
            public Enemy enemyPrefab = null;
            public float secondsToWaitBeforeNextWave = 10f;
        }

        private void Start()
        {
            objectManager = FindObjectOfType<ObjectManager>();
            StartCoroutine(SpawnEnemies());
        }

        private void Update()
        {
            if (objectManager.factories.Count <= 0)
            {
            }
            if (LevelManager.Instance.GetState() == State.End)
            {
                StopAllCoroutines();
                isSpawning = false;
            }
            if (LevelManager.Instance.GetState() == State.Game)
            {
                
            }
        }

        public float CalculateTotalLenght()
        {
            float totalWaveTime = secondsToSpawnAfter;
            foreach (var wave in waves)
            {
                float waveTime = wave.numberOfEnemies * wave.timeBetweenSpawns + wave.secondsToWaitBeforeNextWave;
                totalWaveTime += waveTime;
            }
            Debug.Log(gameObject.name + ": Total time = " + totalWaveTime);

            return totalWaveTime;
        }

        public Waypoint GetStartWaypoint()
        {
            return startWaypoint;
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                
                if (isSpawning && LevelManager.Instance.GetState() == State.Game)
                {
                    if (!hasStartedSpawning)
                    {
                        yield return new WaitForSeconds(secondsToSpawnAfter);
                        hasStartedSpawning = true;
                    }

                    for (int i = 0; i < waves[currentWaveIndex].numberOfEnemies; i++)
                    {
                        Wave currentWave = waves[currentWaveIndex];
                        if (currentWave.altSpawnPoint != null)
                        {
                            Enemy enemy = Instantiate(currentWave.enemyPrefab, currentWave.altSpawnPoint);
                            enemy.transform.parent = transform;
                        }
                        else
                        {
                            Enemy enemy = Instantiate(currentWave.enemyPrefab, transform);
                            enemy.transform.parent = transform;
                        }
                        
                        yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
                    }
                    if (currentWaveIndex + 1 < waves.Length)
                    {
                        yield return new WaitForSeconds(waves[currentWaveIndex].secondsToWaitBeforeNextWave);
                        currentWaveIndex++;
                    }
                    else
                    {
                        isSpawning = false;
                    }
                }
                yield return new WaitForSeconds(0);
            }
        }
    }
}
