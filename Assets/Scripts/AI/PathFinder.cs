using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.Core;

namespace CG.AI
{
    public class PathFinder : ScriptableObject
    {
        Waypoint startWaypoint = null, endWaypoint = null;
        Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
        Dictionary<Waypoint, Waypoint> exploredFrom = new Dictionary<Waypoint, Waypoint>();
        List<Waypoint> exploredWaypoints = new List<Waypoint>();
        Queue<Waypoint> queue = new Queue<Waypoint>();
        bool isRunning = true;
        Waypoint searchCenter;
        List<Waypoint> path = new List<Waypoint>();

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        public List<Waypoint> GetPath(Waypoint startWaypoint, Waypoint endWaypoint)
        {
            this.startWaypoint = startWaypoint;
            this.endWaypoint = endWaypoint;
            CalculatePath();

            Destroy(this, .1f);
            return path;
        }

        private void CalculatePath()
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }


        private void LoadBlocks()
        {
            var waypoints = FindObjectsOfType<Waypoint>();

            foreach (Waypoint waypoint in waypoints)
            {
                var gridPos = waypoint.GetGridPos();
                if (grid.ContainsKey(gridPos))
                {
                    Debug.LogWarning("Skipping overlapping block " + waypoint);
                }
                else if (waypoint.isRoad)
                {
                    grid.Add(waypoint.GetGridPos(), waypoint);
                }
            }
        }


        private void BreadthFirstSearch()
        {
            queue.Enqueue(startWaypoint);

            while (queue.Count > 0 && isRunning)
            {
                searchCenter = queue.Dequeue();
                exploredWaypoints.Add(searchCenter);
                HaltIfEndFound();
                ExploreNeighbours();
            }
        }

        private void HaltIfEndFound()
        {
            if (searchCenter == endWaypoint)
            {
                isRunning = false;
            }
            else
            {
                isRunning = true;
            }
        }

        private void ExploreNeighbours()
        {
            if (!isRunning) { return; }
            foreach (var direction in directions)
            {
                Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
                if (grid.ContainsKey(neighbourCoordinates))
                {
                    QueNewNeighbours(neighbourCoordinates);
                }
            }
        }

        private void QueNewNeighbours(Vector2Int neighbourCoordinates)
        {
            Waypoint neighbour = grid[neighbourCoordinates];
            if (exploredWaypoints.Contains(neighbour) || queue.Contains(neighbour))
            {
                return;
            }
            else
            {
                queue.Enqueue(neighbour);
                exploredFrom.Add(neighbour, searchCenter);
            }

        }

        private void CreatePath()
        {
            SetAsPath(endWaypoint);

            Waypoint previous = null;
            exploredFrom.TryGetValue(endWaypoint, out previous);

            while (previous != startWaypoint)
            {
                SetAsPath(previous);
                exploredFrom.TryGetValue(previous, out previous);
            }

            SetAsPath(startWaypoint);
            path.Reverse();

        }

        private void SetAsPath(Waypoint waypoint)
        {
            path.Add(waypoint);
        }
    }

}
