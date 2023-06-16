//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class AttractRepelSpline : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private SplineContainer splineContainer;
    public SplineContainer SplineContainer => splineContainer;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        splineContainer = GetComponent<SplineContainer>();
    }
    #endif
}