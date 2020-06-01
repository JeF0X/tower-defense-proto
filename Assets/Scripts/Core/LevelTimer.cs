using CG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.Core
{

    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] float timer = 120f;

        bool scoreCounted = false;

        private void Start()
        {
            scoreCounted = false;
            var enemySpawners = FindObjectsOfType<EnemySpawner>();
            float longestSpawnerTime = 0;
            foreach (var enemySpawner in enemySpawners)
            {
                float newSpawnerTime = enemySpawner.CalculateTotalLenght();
                if (newSpawnerTime > longestSpawnerTime)
                {
                    longestSpawnerTime = newSpawnerTime;
                }
            }
            timer = longestSpawnerTime;
        }
        private void Update()
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            if (LevelManager.Instance.GetState() == State.Game)
            {
                if (timer >= 0f)
                {
                    timer -= Time.deltaTime;
                }
                
            }


            if (scoreCounted)
            {
                return;
            }

            if (timer <= 0f && ObjectManager.Instance.enemies.Count == 0 || LevelManager.Instance.GetState() == State.End)
            {
                
                //FindObjectOfType<Score>().CalculateScore();
                scoreCounted = true;
                StartCoroutine(EndGame());
            }
        }

        public float GetTime()
        {
            return timer;
        }

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(2f);
            LevelManager.Instance.SetState(State.End);
            Time.timeScale = 0;
        }
    }
}
