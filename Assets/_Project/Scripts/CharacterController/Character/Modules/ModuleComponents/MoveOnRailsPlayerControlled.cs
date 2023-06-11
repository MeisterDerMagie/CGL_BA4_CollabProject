using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class MoveOnRailsPlayerControlled : MoveOnRails
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(Walk),
            typeof(Teleport),
            typeof(MoveOnRailsAnimated),
            typeof(MoveOnRailsScripted)
        };

    [SerializeField][SuffixLabel("Meter per second")]
    private float speed = 5;

    [SerializeField]
    private bool loop = true;

    [SerializeField][Range(0f, 1f)]
    private float startOffset;

    private void Awake() => onEnabledChanged += PauseRailsOnEnable;
    private void OnDestroy() => onEnabledChanged -= PauseRailsOnEnable;

    private void Start()
    {
        if (!IsEnabled) return;

        PauseRails();
    }

    private void Update()
    {
        //update the position in Edit Mode
        if (Rails == null) return;
        if (Application.isPlaying || !IsEnabled) return;

        if (Rails.StartOffset != startOffset)
        {
            Rails.StartOffset = startOffset;
            Rails.NormalizedTime = startOffset;
            #if UNITY_EDITOR
            if(!Application.isPlaying) PrefabUtility.RecordPrefabInstancePropertyModifications(Rails.Target.GetComponent<SplineAnimate>());
            #endif
        }

        GlueCharacterToRails();
    }

    public void ExecuteMovement(float movementDirection)
    {
        if (!IsEnabled)
            return;
        
        //calculate travelled distance
        float currentDistance = Rails.Length * Rails.NormalizedTime;
        float moveDistance = movementDirection * speed * Time.deltaTime;
        float newDistance = currentDistance + moveDistance;
        float newNormalizedTime = newDistance / Rails.Length;
        
        //Clamp normalized time
        if (!loop) newNormalizedTime = Mathf.Clamp(newNormalizedTime, 0f, 1f);
        else if (newNormalizedTime < 0f) newNormalizedTime = 1f - newNormalizedTime;
        else if (newNormalizedTime > 1f) newNormalizedTime = newNormalizedTime - 1f;

        //set normalized time
        Rails.NormalizedTime = newNormalizedTime;
        
        //update player position and rotation
        GlueCharacterToRails();
    }

    private void PauseRailsOnEnable(bool isEnabled)
    {
        if (isEnabled) PauseRails();
    }

    private void PauseRails()
    {
        if(Rails != null) Rails.Pause();
    }
}