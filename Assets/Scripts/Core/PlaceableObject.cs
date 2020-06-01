using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CG.Core;
using CG.UI;
using CG.Player;
using System;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] float cost = 100f;
    public bool canSpawnOnRoads = false;

    Waypoint baseWaypoint = null;
    List<List<Color>> origColors = new List<List<Color>>();
    public bool actionsEnabled = true;

    private void Start()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        if (LevelManager.Instance.isInBulldozeMode)
        {
            GetComponent<Collider>().enabled = true;
        }
        else if(actionsEnabled == false)
        {
           GetComponent<Collider>().enabled = false;
        }
    }

    private void OnMouseOver()
    {
        if (GetComponent<HomeBase>())
        {
            return;
        }
        if (LevelManager.Instance.isInBulldozeMode)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(gameObject);
            }
            ChangeColor();
        }
    }

    private void OnMouseExit()
    {
        if (GetComponent<HomeBase>())
        {
            return;
        }

        if (LevelManager.Instance.isInBulldozeMode)
        {
            SetDefaultColors();
        }
        
    }

    private void SetDefaultColors()
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for (int meshIndex = 0; meshIndex < meshes.Length; meshIndex++)
        {
            Material[] materials = meshes[meshIndex].materials;
            for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
            {
                materials[materialIndex].color = origColors[meshIndex][materialIndex];
            }
        }
    }

    private void ChangeColor()
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for (int meshIndex = 0; meshIndex < meshes.Length; meshIndex++)
        {
            List<Color> colors = new List<Color>();
            Material[] materials = meshes[meshIndex].materials;
            for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
            {
                colors.Add(materials[materialIndex].color);
                materials[materialIndex].color = Color.red;
            }
            origColors.Add(colors);
        }
    }

    public void SetWaypoint(Waypoint waypoint)
    {
        baseWaypoint = waypoint;
    }

    public Waypoint GetWaypoint()
    {
        return baseWaypoint;
    }

    public float GetCost()
    {
        return cost;
    }

    public void DisableActions()
    {
        actionsEnabled = false;
    }

    private void OnDestroy()
    {
        if (actionsEnabled && baseWaypoint != null)
        {
            baseWaypoint.isRoad = false;
            baseWaypoint.isPlaceable = true;
        }
    }

    public IEnumerator ChangeColorOnHit()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeColor();
        yield return new WaitForSeconds(0.1f);
        SetDefaultColors();
    }
}
