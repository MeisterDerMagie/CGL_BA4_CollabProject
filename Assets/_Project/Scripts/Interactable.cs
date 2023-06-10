//(c) copyright by Martin M. Klöckener
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : Focusable
{
    [PropertyOrder(10)]
    [Min(0)][BoxGroup("How many times can the player interact with the object? 0 means infinite")][DisableInPlayMode]
    [SerializeField] private int _maxInteractionCount = 1; //0 means infinite

    [PropertyOrder(11)]
    [BoxGroup("Override the default interaction duration")] [DisableInPlayMode]
    [SerializeField] private bool _overrideInteractionDuration;

    [PropertyOrder(12)]
    [BoxGroup("Override the default interaction duration")] [DisableInPlayMode]
    [ShowIf(nameof(_overrideInteractionDuration))]
    [Min(0f)]
    [SerializeField] private float _customInteractionDuration = 3f;

    public bool OverrideInteractionDuration => _overrideInteractionDuration;
    public float CustomInteractionDuration => _customInteractionDuration;
    
    [PropertyOrder(15)]
    [Range(0f, 1f)][ReadOnly]
    public float interactionProgress;
    
    private bool _hadInteractionSinceBeginHover = false;
    private bool _hasReachedMaxActivations = false;

    private int _totalInteractions = 0;

    [PropertyOrder(60)]
    [SerializeField]
    private UnityEvent<int> onInteract; //passes along the total amount of interactions

    public override void EndFocus()
    {
        base.EndFocus();
        
        interactionProgress = 0f;
        _hadInteractionSinceBeginHover = false;
    }

    public override void SetEnabled(bool isEnabled)
    {
        if (isEnabled && !_hasReachedMaxActivations)
        {
            _isEnabled = true;
            onBecameEnabled.Invoke();
            return;
        }

        _isEnabled = false;
        onBecameDisabled.Invoke();
    }

    public void Interact()
    {
        if (!_isEnabled)
            return;

        if (_hasReachedMaxActivations)
            return;
        
        if (_hadInteractionSinceBeginHover)
            return;
        
        //Debug.Log($"Interact with game object: {gameObject.name}");
        
        _totalInteractions += 1;
        if (_maxInteractionCount != 0 && _totalInteractions >= _maxInteractionCount)
        {
            _hasReachedMaxActivations = true;
            SetEnabled(false);
        }
        _hadInteractionSinceBeginHover = true;
        onInteract.Invoke(_totalInteractions);
    }
}