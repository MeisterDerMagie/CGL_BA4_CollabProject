//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

[ExecuteInEditMode]
[RequireComponent(typeof(SplineAnimate))]
public class SplineAnimationProgress : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private SplineAnimate splineAnimate;

    [SerializeField][Range(0f, 1f)]
    private float progress;
    
    private void Update()
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        splineAnimate.NormalizedTime = progress;
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }
    #endif
}