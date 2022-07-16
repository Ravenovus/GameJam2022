using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer lineRenderer;

    [SerializeField] float velocity;
    [SerializeField] float angle;
    [SerializeField] int resolution = 30;

    private float gravity;
    private float radianAngle;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gravity = Mathf.Abs(Physics2D.gravity.y);      
    }

    //private void OnValidate()
    //{
    //    if (lineRenderer != null && Application.isPlaying)
    //    {
    //        RenderArc();
    //    }
    //}

    //private void Start()
    //{
    //    RenderArc();
    //}

    public void RenderArc(Vector2 startingPosition)
    {
        radianAngle = Mathf.Atan2(startingPosition.y, startingPosition.x);
        Debug.Log(radianAngle);
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.SetPositions(CalculateArc(startingPosition));
        
    }

    private Vector3[] CalculateArc(Vector2 startingPosition)
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        //radianAngle = Mathf.Deg2Rad * angle;      
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gravity;
        //Debug.Log("Max distance is = " + maxDistance);

        for(int i = 0; i <= resolution; i++)
        {
            //Debug.Log("The I is = " + i + " The resolution is = " + resolution);
            float t = (float)i / (float)resolution;
            //Debug.Log(t);
            arcArray[i] = CalculatePoint(t, maxDistance, startingPosition);
            
        }


        return arcArray;
    }

    private Vector3 CalculatePoint(float t, float maxDistance, Vector2 startingPosition)
    {
        float x = (t * maxDistance) + Mathf.Abs(startingPosition.x);
        float y = (float)((x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Math.Cos(radianAngle) * Mathf.Cos(radianAngle))))+Mathf.Abs(startingPosition.y));
        return new Vector3(x, y);
    }
}
