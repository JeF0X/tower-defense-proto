using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CG.Core;

namespace CG.UI
{

    public class GameUIHandler : MonoBehaviour
    {
        [SerializeField] TMP_Text timerText = null;
        [SerializeField] TMP_Text pointsText = null;
        [SerializeField] TMP_Text homebaseHealthText = null;
        [SerializeField] TMP_Text endTotalScoreText = null;
        [SerializeField] TMP_Text PlaceHomebaseText = null;
        [SerializeField] PlaceableObjectButton[] buttons = null;
        [SerializeField] Color inactiveButtonColor = Color.grey;
        [SerializeField] Canvas gameCanvas = null;
        [SerializeField] Canvas gameOverCanvas = null;
        [SerializeField] Canvas pauseCanvas = null;
        [SerializeField] Slider volumeSlider = null;
        

        PlaceableObjectButton activeButton = null;
        LevelTimer levelTimer = null;
        Score score = null;
        MusicPlayer musicPlayer = null;



        private void Start()
        {
            levelTimer = FindObjectOfType<LevelTimer>();
            score = FindObjectOfType<Score>();
            musicPlayer = FindObjectOfType<MusicPlayer>();

            foreach (var butt in buttons)
            {
                butt.DisableButton();
            }

            gameOverCanvas.enabled = false;
            gameCanvas.enabled = true;
            pauseCanvas.enabled = false;
            if (musicPlayer)
            {
                volumeSlider.value = musicPlayer.GetVolume();
            }
        }

        private void Update()
        {
            if (musicPlayer)
            {
                musicPlayer.SetVolume(volumeSlider.value);
            }
            
            if (LevelManager.Instance.GetState() == State.Start)
            {
                PlaceHomebaseText.enabled = true;
            }
            else
            {
                PlaceHomebaseText.enabled = false;
            }

            if (LevelManager.Instance.GetState() == State.Game)
            {
                gameOverCanvas.enabled = false;
                gameCanvas.enabled = true;
                pauseCanvas.enabled = false;
            }

            if (LevelManager.Instance.GetState() == State.End)
            {
                gameOverCanvas.enabled = true;
                gameCanvas.enabled = false;
                int finalScore = Mathf.RoundToInt(score.GetTotalScore());
                endTotalScoreText.text = finalScore.ToString();
            }

            if (LevelManager.Instance.GetState() == State.Pause)
            {
                gameOverCanvas.enabled = false;
                gameCanvas.enabled = true;
                pauseCanvas.enabled = true;
            }

            UpdatePoints();
            UpdateTimer();
            homebaseHealthText.text = Mathf.RoundToInt(score.GetHomeBaseHealth()).ToString();

            if (LevelManager.Instance.GetState() == State.End)
            {
                FindObjectOfType<GameOverUIHandler>().EnablePointsCanvas();
            }
        }

        private void UpdatePoints()
        {
            pointsText.text = Mathf.RoundToInt(score.GetTotalScore()).ToString();
        }

        private void UpdateTimer()
        {
            timerText.text = Mathf.RoundToInt(levelTimer.GetTime()).ToString();
        }

        public void SetButtonsDefaultColor()
        {
            foreach (var butt in buttons)
            {
                butt.EnableButton();
            }
        }

        public void SetActiveButton(PlaceableObjectButton button)
        {
            foreach (var butt in buttons)
            {
                butt.isActive = false;
            }
            if (activeButton == button)
            {
                activeButton = null;
                FindObjectOfType<PlayerSpawner>().SetActivePlaceableObject(null);
                return;
            }
            activeButton = button;
            activeButton.isActive = true;
            activeButton.SetActivePrefab();
        }

        public void DeactivateButton(PlaceableObjectButton button)
        {
            if (activeButton == button)
            {
                activeButton = null;
                FindObjectOfType<PlayerSpawner>().SetActivePlaceableObject(null);
                return;
            }
        }

        public void RestartLevel()
        {
            SceneLoader.Instance.ReloadLevel();
        }

        public void GoToMainMenu()
        {
            SceneLoader.Instance.LoadMainMenu();
        }

    }

}