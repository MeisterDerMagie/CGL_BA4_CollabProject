using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MoveOnRailsAnimated : MoveOnRails
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(Walk),
            typeof(Teleport),
            typeof(MoveOnRailsPlayerControlled),
            typeof(MoveOnRailsScripted)
        };

    [SerializeField]
    private bool playOnAwake;
    
    //even though it says play on Awake, putting it into Awake didn't work... 
    private void Start()
    {
        if(playOnAwake && IsEnabledAndRailsAreNotNull())
            Play();
    }

    private void Update()
    {
        if (!IsEnabledAndRailsAreNotNull()) return;

        GlueCharacterToRails();
    }

    public void Play()
    {
        if (!IsEnabledAndRailsAreNotNull()) return;
        Rails.Play();
    }

    public void Pause() => Rails.Pause();
    public void Restart()
    {
        if (!IsEnabledAndRailsAreNotNull()) return;
        Rails.Restart();
    }
    public void Stop() => Rails.Stop();

    private bool IsEnabledAndRailsAreNotNull()
    {
        if (!IsEnabled) return false;
        if (Rails != null) return true;
        
        //Debug.LogError("Rails can't be null!", this);
        return false;
    }

    private void OnValidate()
    {
        if (Rails == null) return;

        Rails.NormalizedTime = Rails.StartOffset;
        GlueCharacterToRails();
    }
}
