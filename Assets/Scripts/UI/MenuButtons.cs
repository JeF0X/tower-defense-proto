using CG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CG.UI
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] Canvas mainMenuCanvas = null;
        [SerializeField] Canvas optionsCanvas = null;
        [SerializeField] Slider volumeSlider = null;

        MusicPlayer musicPlayer = null;

        private void Start()
        {
            optionsCanvas.enabled = false;
            musicPlayer = FindObjectOfType<MusicPlayer>();
            volumeSlider.value = musicPlayer.GetVolume();
        }

        private void Update()
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }

        public void LoadFirstScene()
        {
            SceneLoader.Instance.LoadLevel(1);
        }

        public void ExitToMainMenu()
        {
            SceneLoader.Instance.LoadLevel(0);
        }

        public void ToggleOptions()
        {
            mainMenuCanvas.enabled = optionsCanvas.enabled;
            optionsCanvas.enabled = !optionsCanvas.enabled;
        }

        public void ExitGame()
        {
            SceneLoader.Instance.ExitApplication();
        }
    }

}
