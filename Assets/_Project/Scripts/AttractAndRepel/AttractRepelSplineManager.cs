//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Wichtel;

public class AttractRepelSplineManager : SingletonBehaviour<AttractRepelSplineManager>
{
    [SerializeField][HideInEditorMode]
    private List<AttractRepelSpline> splines = new();

    private void Awake()
    {
        InitSingleton();
        
        FindAllSplines();
    }

    public (Vector3 point, float distance) GetNearestPointOnSpline(Vector3 playerPosition)
    {
        (Vector3 point, float distance) nearestPoint = (Vector3.zero, float.PositiveInfinity);
        
        foreach (var spline in splines)
        {
            float distance = SplineUtility.GetNearestPoint(spline.SplineContainer.Spline, (float3)playerPosition, out float3 nearestPointOnSpline, out float normalizedInterpolationRatio);
            if (distance < nearestPoint.distance) nearestPoint = (nearestPointOnSpline, distance);
        }

        return nearestPoint;
    } 

    private void FindAllSplines()
    {
        splines.Clear();

        AttractRepelSpline[] allSplines = FindObjectsOfType<AttractRepelSpline>(true);
        
        foreach (var spline in allSplines)
        {
            splines.Add(spline);
        }
    }
}