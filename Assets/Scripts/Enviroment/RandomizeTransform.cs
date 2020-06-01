using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomizeTransform : MonoBehaviour
{
    [Range(0.1f, 500f)] [SerializeField] float minScale = 1;
    [Range(0.1f, 500f)] [SerializeField] float maxScale = 1;
    [Header("Rotate Axis:")]
    [SerializeField] bool RotateXAxis = false;
    [SerializeField] bool RotateYAxis = false;
    [SerializeField] bool RotateZAxis = false;
    [Header("Buttons")]
    [SerializeField] bool randomRotation = false;
    [SerializeField] bool randomScale = false;

    private float currentScale;
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
        RandomizeScale();
        RandomizeRotation();
    }

    private void Update()
    {
        if (minScale > maxScale)
        {
            maxScale = minScale;
            RandomizeScale();
        }
        if (maxScale < minScale)
        {
            minScale = maxScale;
            RandomizeScale();
        }

        if (currentScale < minScale || currentScale > maxScale)
        {
            RandomizeScale();
        }

        if (randomRotation)
        {
            randomRotation = false;
            RandomizeRotation();
        }

        if (randomScale)
        {
            randomScale = false;
            RandomizeScale();
        }
    }

    private void RandomizeScale()
    {
        currentScale = Random.Range(minScale, maxScale);
        transform.localScale = Vector3.one * currentScale;
    }

    private void RandomizeRotation()
    {
        transform.rotation = originalRotation;
        if (RotateXAxis)
        {
            transform.Rotate(Random.Range(-180f, 180f), 0f, 0f, Space.World);
        }
        if (RotateYAxis)
        {
            transform.Rotate(0f, Random.Range(-180f, 180f), 0f, Space.World);
        }
        if (RotateZAxis)
        {
            transform.Rotate(0f, 0f, Random.Range(-180f, 180f), Space.World);
        }
        
    }


}
