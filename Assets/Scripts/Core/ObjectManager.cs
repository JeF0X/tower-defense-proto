using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.Combat;

namespace CG.Core
{
    public class ObjectManager : MonoBehaviour
    {
        public List<Transform> enemies;
        public List<Transform> placeableObjects;
        public List<PlaceableObject> factories;
        public List<Transform> blockers;
        public int maxBlockers = 1;

        private static ObjectManager _instance;
        

        public static ObjectManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public List<Transform> GetTypesOf(Type type)
        {
            if (type == typeof(Enemy))
            {
                return enemies;
            }
            else if (type == typeof(PlaceableObject))
            {
                return placeableObjects;
            }
            else
            {
                Debug.LogError("Couldn't find objects of type: " + type);
                return null;
            }
        }
    }
}
