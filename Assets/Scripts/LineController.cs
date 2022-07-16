using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] int positionAmount = 2;
    private LineRenderer lineRenderer;
    private Transform target;




    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //float width = lineRenderer.startWidth;
        //lineRenderer.material.mainTextureScale = new Vector2(1f / width, 1.0f);
        //if (!finishedSetUp)
        //{
        //    lineRenderer.SetPosition(lineRenderer.positionCount - 1, target.position);
        //    Vector3 start = lineRenderer.GetPosition(0);
            
        //}
        //finishedSetUp = true;
    }

    public void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
        lineRenderer.positionCount = positionAmount;
        lineRenderer.SetPosition(0, startPosition);       
        target = newTarget;
        lineRenderer.SetPosition(1, target.position);

    }

    public void SelfDestruction()
    {
        Destroy(gameObject);
    }




}
