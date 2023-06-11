using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SplineContainer))]
[ExecuteInEditMode]
public class Rails : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private SplineContainer spline;

    [SerializeField][HideInInspector]
    private SplineAnimate animate;

    public Transform Target => animate == null ? null : animate.transform;

    public float NormalizedTime
    {
        get => animate == null ? 0f : animate.NormalizedTime;
        set
        {
            animate.NormalizedTime = value;
            #if UNITY_EDITOR
            if(!Application.isPlaying) PrefabUtility.RecordPrefabInstancePropertyModifications(animate);
            EditorUtility.SetDirty(animate);
            #endif
        }
    }

    public float Length => spline.CalculateLength();
    public float StartOffset
    {
        get => animate.StartOffset;
        set => animate.StartOffset = value;
    }

    public void Play() => animate.Play();
    public void Pause() => animate.Pause();
    public void Restart() => animate.Restart(true);
    public void Stop() => animate.Restart(false);

    #if UNITY_EDITOR
    private void OnValidate()
    {
        spline = GetComponent<SplineContainer>();
        animate = GetComponentInChildren<SplineAnimate>(true);
        if (animate != null) animate.Container = spline;
        else Debug.LogWarning("Rails need a Target! (A child object with the SplineAnimate component on it)");
    }
    #endif
}
