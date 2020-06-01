using CG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Blocker : MonoBehaviour
{
    ObjectManager objectManager;

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    private void Update()
    {
        GetComponent<NavMeshObstacle>().enabled = GetComponent<PlaceableObject>().actionsEnabled;
    }

    private void OnDestroy()
    {
        try
        {
            objectManager.placeableObjects.Remove(transform);
            objectManager.blockers.Remove(transform);
        }
        catch (Exception)
        {
            Debug.LogWarning(name + " could not be removed from object list");
        }
    }
}
