using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CG.Core
{
    public class Waypoint : MonoBehaviour
    {
        public bool isExplored;
        public Waypoint exploredFrom;
        public bool isPlaceable = true;
        public bool isRoad = false;

        Vector2Int gridPos;
        private Waypoint origWaypoint;
        const int gridSize = 10;

        PlayerSpawner playerSpawner;

        private void Start()
        {
            playerSpawner = FindObjectOfType<PlayerSpawner>();
        }

        //TODO: redo mouse over
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerSpawner.AddPlaceableObject(this);
            }
            else
            {
                playerSpawner.HoverPlaceableObject(this);
            }
        }

        public int GetGridSize()
        {
            return gridSize;
        }

        public Vector2Int GetGridPos()
        {
            return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize));
        }
    }
}
