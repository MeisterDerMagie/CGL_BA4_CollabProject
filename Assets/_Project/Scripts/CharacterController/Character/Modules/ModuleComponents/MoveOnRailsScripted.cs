//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class MoveOnRailsScripted : MoveOnRails
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(Walk),
            typeof(Teleport),
            typeof(MoveOnRailsAnimated),
            typeof(MoveOnRailsPlayerControlled),
            typeof(PushAndPull)
        };
    
    [Range(0f, 1f)]
    public float progress;
    
    private void Awake() => onEnabledChanged += StopRailsOnEnable;
    private void OnDestroy() => onEnabledChanged -= StopRailsOnEnable;
    
    private void Start()
    {
        if (IsEnabled)
            Timing.RunCoroutine(_StartDelayed());
    }

    private IEnumerator<float> _StartDelayed()
    {
        yield return Timing.WaitForOneFrame;
        Rails.Stop();
    }
    
    private void Update()
    {
        if (!IsEnabled) return;
        if (Rails == null) return;
        
        Rails.NormalizedTime = progress;
        
        GlueCharacterToRails();
    }
    
    private void StopRailsOnEnable(bool isEnabled)
    {
        if (isEnabled && Rails != null) Rails.Stop();
    }
}