using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.UI
{
    public class GameOverUIHandler : MonoBehaviour
    {
        [SerializeField] Canvas pointsCanvas = null;

        GameUIHandler gameUIHandler;
        bool hasRun = false;

        private void Start()
        {
            gameUIHandler = FindObjectOfType<GameUIHandler>();
            DisableCanvases();

        }

        public void EnablePointsCanvas()
        {
            if (hasRun)
            {
                return;
            }
            else
            {
                pointsCanvas.enabled = true;
            }
            hasRun = true;
        }

        private void DisableCanvases()
        {
            pointsCanvas.enabled = false;
        }

    }
}

