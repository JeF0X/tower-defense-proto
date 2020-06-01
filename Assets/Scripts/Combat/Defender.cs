using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CG.Player
{
    public class Defender : MonoBehaviour
    {
        ObjectManager objectManager;

        private void Awake()
        {
            objectManager = FindObjectOfType<ObjectManager>();
        }

        private void OnDestroy()
        {
            try
            {
                objectManager.placeableObjects.Remove(transform);
            }
            catch (Exception)
            {
                Debug.LogWarning(name + " could not be removed from object list");
            }
        }
    }
}