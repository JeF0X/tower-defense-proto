using CG.Core;
using CG.Player;
using CG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.Core
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] PlaceableObject Homebase = null;
        [SerializeField] Color hoverHighlightColor;

        PlaceableObject placeableObject = null;
        PlaceableObject hoverPlaceableObject = null;
        ObjectManager objectManager = null;
        Queue<PlaceableObject> factoryQueue = new Queue<PlaceableObject>();
        Score score = null;
        public bool isBuldozeMode = false;

        private void Start()
        {
            objectManager = FindObjectOfType<ObjectManager>();
            score = FindObjectOfType<Score>();
            if (LevelManager.Instance.GetState() == State.Start)
            {
                SetActivePlaceableObject(Homebase);
            }
        }


        public void SetActivePlaceableObject(PlaceableObject placeableObjectPrefab)
        {
            placeableObject = placeableObjectPrefab;
            if (hoverPlaceableObject != null)
            {
                Destroy(hoverPlaceableObject.gameObject);
                hoverPlaceableObject = null;
            }
        }

        public void AddPlaceableObject(Waypoint waypoint)
        {
            if (placeableObject == null) { return; }

            if (!waypoint.isPlaceable)
            {
                return;
            }

            if (placeableObject.GetCost() > score.GetTotalScore())
            {
                SetActivePlaceableObject(null);
                Debug.Log("Not enough points!");
                return;
            }

            if (placeableObject.GetComponent<Blocker>() && objectManager.blockers.Count >= objectManager.maxBlockers)
            {
                Debug.Log("Can't add more blockers");
                return;
            }

            if (placeableObject.canSpawnOnRoads && !waypoint.isRoad)
            {
                Debug.Log(placeableObject.name + ": Can't place here: " + waypoint.name);
                return;

            }
            else if (!placeableObject.canSpawnOnRoads && waypoint.isRoad)
            {
                Debug.Log(placeableObject.name + ": Can't place here: " + waypoint.name);
                return;
            }

            else if (placeableObject.canSpawnOnRoads && waypoint.isRoad)
            {
                FindObjectOfType<Score>().DecreasePoints(placeableObject.GetCost());
                InstantiateNewObject(waypoint);
                return;
            }

            else
            {
                FindObjectOfType<Score>().DecreasePoints(placeableObject.GetCost());
                InstantiateNewObject(waypoint);
                if (hoverPlaceableObject != null)
                {
                    Destroy(hoverPlaceableObject.gameObject);
                    hoverPlaceableObject = null;
                } 
            }
        }

        public void HoverPlaceableObject(Waypoint waypoint)
        {
            if (placeableObject == null) { return; }

            if (!waypoint.isPlaceable)
            {
                return;
            }

            if (placeableObject.GetCost() > score.GetTotalScore())
            {
                SetActivePlaceableObject(null);
                return;
            }

            if (placeableObject.GetComponent<Blocker>() && objectManager.blockers.Count >= objectManager.maxBlockers)
            {
                return;
            }

            if (placeableObject.canSpawnOnRoads && waypoint.isRoad)
            {
                if (hoverPlaceableObject == null)
                {
                    hoverPlaceableObject = Instantiate(placeableObject, waypoint.transform.position, Quaternion.identity);
                    ChangeHoverColor();
                    hoverPlaceableObject.actionsEnabled = false;
                    hoverPlaceableObject.GetComponent<Collider>().enabled = false;
                }

                else
                {
                    hoverPlaceableObject.GetComponent<Collider>().enabled = false;
                    hoverPlaceableObject.transform.position = waypoint.transform.position;
                }
            }

            

            if (!waypoint.isPlaceable)
            {
                return;
            }
            else if (placeableObject.canSpawnOnRoads && !waypoint.isRoad)
            {
                return;
            }
            else
            {
                if (waypoint.isRoad)
                {
                    return;
                }
                if (hoverPlaceableObject == null)
                {
                    hoverPlaceableObject = Instantiate(placeableObject, waypoint.transform.position, Quaternion.identity);
                    ChangeHoverColor();
                    hoverPlaceableObject.actionsEnabled = false;
                    hoverPlaceableObject.GetComponent<Collider>().enabled = false;
                }

                else
                {
                    hoverPlaceableObject.GetComponent<Collider>().enabled = false;
                    hoverPlaceableObject.transform.position = waypoint.transform.position;
                    
                }
            }
        }

        private void ChangeHoverColor()
        {
            
            MeshRenderer[] meshes = hoverPlaceableObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshes)
            {
                Material[] materials = mesh.materials;
                foreach (var material in materials)
                {
                    material.color = hoverHighlightColor;
                }
            }
        }

        private void InstantiateNewObject(Waypoint waypoint)
        {
            if (!waypoint.isPlaceable)
            {
                Debug.Log("Waypoint: " + waypoint + " is not placeable");
                return;
            }
            PlaceableObject newPlaceableObject = Instantiate(placeableObject, waypoint.transform.position, Quaternion.identity);
            newPlaceableObject.transform.parent = transform;
            newPlaceableObject.SetWaypoint(waypoint);
            factoryQueue.Enqueue(newPlaceableObject);
            waypoint.isPlaceable = false;
            newPlaceableObject.GetComponent<Collider>().enabled = true;
            //waypoint.isRoad = true;

            objectManager.placeableObjects.Add(newPlaceableObject.transform);
            if (newPlaceableObject.GetComponent<Factory>())
            {
                objectManager.factories.Add(newPlaceableObject);
            }
            if (newPlaceableObject.GetComponent<HomeBase>())
            {
                objectManager.factories.Add(newPlaceableObject);
                newPlaceableObject.GetComponent<HomeBase>().isHomebase = true;
                LevelManager.Instance.SetState(State.Game);
                FindObjectOfType<GameUIHandler>().SetButtonsDefaultColor();
                SetActivePlaceableObject(null);
            }
            if (newPlaceableObject.GetComponent<Blocker>())
            {
                objectManager.blockers.Add(newPlaceableObject.transform);
            }
        }

        public PlaceableObject GetNearestFactory(Waypoint waypoint)
        {
            float closestWaypontDistance = Mathf.Infinity;
            PlaceableObject closestWaypoint = null;
            List<PlaceableObject> factoriesCleaned = new List<PlaceableObject>();

            foreach (PlaceableObject factory in objectManager.factories)
            {
                if (factory == null)
                {
                    continue;
                }
                factoriesCleaned.Add(factory);
            }

            foreach (PlaceableObject factory in factoriesCleaned)
            {
                var currentComparison = Vector3.Distance(factory.GetWaypoint().transform.position, waypoint.transform.position);
                if (currentComparison < closestWaypontDistance)
                {
                    closestWaypontDistance = currentComparison;
                    closestWaypoint = factory;
                }
            }
            return closestWaypoint;
        }
    }
}