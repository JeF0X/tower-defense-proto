using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CG.Core;

namespace CG.UI
{
    public class BlockerButton : MonoBehaviour
    {
        [SerializeField] TMP_Text blockersText;

        ObjectManager objectManager = null;

        private void Start()
        {
            objectManager = FindObjectOfType<ObjectManager>();
        }

        private void Update()
        {
            UpdateBlocersText();
        }

        private void UpdateBlocersText()
        {
            blockersText.text = objectManager.blockers.Count.ToString() + "/" + objectManager.maxBlockers.ToString();
        }
    }

}
