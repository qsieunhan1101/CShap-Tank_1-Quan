using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectotyLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField, Min(3)] private int lineSegments = 60;
    [SerializeField, Min(1)] private float timeOfTheFlight = 5;


    
    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity)
    {
        float timeStep = timeOfTheFlight / lineSegments;

        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity, timeStep);

        lineRenderer.positionCount = lineSegments;
        lineRenderer.SetPositions(lineRendererPoints);
    }
    
    private Vector3[] CalculateTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, float timeStep)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegments];

        lineRendererPoints[0] = startPoint;

        for (int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressBeforeGravity = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = startPoint + progressBeforeGravity - gravityOffset;
            lineRendererPoints[i] = newPosition;
        }

        return lineRendererPoints;
    }
    

    // moi tuong quan giua khoang cach va luc ban: luc ban/khoang cach ~ 44 donvi
}   
