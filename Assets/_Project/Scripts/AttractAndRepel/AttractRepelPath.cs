//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class AttractRepelPath : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private SplineContainer splineContainer;
    public SplineContainer SplineContainer => splineContainer;
    
    #if UNITY_EDITOR
    private void Reset()
    {
        GetSplineContainer();
        splineContainer.Spline.Clear();
        splineContainer.Spline.Add(new BezierKnot(new float3(0f, 0f, 0f)));
        splineContainer.Spline.Add(new BezierKnot(new float3(5f, 0f, 0f)));
    }

    private void GetSplineContainer() => splineContainer = GetComponent<SplineContainer>();
    #endif
}