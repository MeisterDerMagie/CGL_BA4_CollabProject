using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

[DisallowMultipleComponent]
public class MoveOnRailsAnimated : MoveOnRails
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(Walk),
            typeof(Teleport),
            typeof(MoveOnRailsPlayerControlled),
            typeof(MoveOnRailsScripted),
            typeof(AttractAndRepel)
        };

    [SerializeField]
    private bool playOnAwake;

    private void Awake() => onEnabledChanged += PauseOnDisable;
    private void OnDestroy() => onEnabledChanged -= PauseOnDisable;

    //even though it says play on Awake, putting it into Awake didn't work... 
    private void Start()
    {
        if (IsEnabledAndRailsAreNotNull())
            Timing.RunCoroutine(_StartDelayed());
    }

    private IEnumerator<float> _StartDelayed()
    {
        yield return Timing.WaitForOneFrame;
        
        if(playOnAwake) Play();
    }

    private void Update()
    {
        if (!IsEnabledAndRailsAreNotNull()) return;

        GlueCharacterToRails();
    }

    [Button][HideInEditorMode]
    public void Play()
    {
        if (!IsEnabledAndRailsAreNotNull()) return;
        Rails.Play();
    }

    [Button][HideInEditorMode]
    public void Pause()
    {
        if(Rails != null) Rails.Pause();
    }

    [Button][HideInEditorMode]
    public void Stop()
    {
        if(Rails != null) Rails.Stop();
    }

    [Button][HideInEditorMode]
    public void Restart()
    {
        if (!IsEnabledAndRailsAreNotNull()) return;
        Rails.Restart();
    }

    private bool IsEnabledAndRailsAreNotNull()
    {
        if (!IsEnabled) return false;
        if (Rails != null) return true;
        
        //Debug.LogError("Rails can't be null!", this);
        return false;
    }
    
    private void PauseOnDisable(bool isEnabled)
    {
        if(!isEnabled) Pause();
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Rails == null) return;
        if (Application.isPlaying) return;

        Rails.PlayOnAwake = playOnAwake;
        if(!Application.isPlaying) UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(Rails.Target.GetComponent<SplineAnimate>());
    }
    #endif
}
