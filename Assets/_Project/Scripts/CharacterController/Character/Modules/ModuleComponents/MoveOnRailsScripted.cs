//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
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
            typeof(MoveOnRailsPlayerControlled)
        };
    
    [Range(0f, 1f)]
    public float progress;
    
    private void Awake() => onEnabledChanged += PauseRailsOnEnable;
    private void OnDestroy() => onEnabledChanged -= PauseRailsOnEnable;
    
    private void Update()
    {
        if (!IsEnabled) return;
        if (Rails == null) return;
        
        Rails.NormalizedTime = progress;
        
        GlueCharacterToRails();
    }
    
    private void PauseRailsOnEnable(bool isEnabled)
    {
        if (isEnabled) Rails.Pause();
    }
}