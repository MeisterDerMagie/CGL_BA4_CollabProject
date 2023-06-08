//(c) copyright by Martin M. Klöckener
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour
{
    [SerializeField][DisableInPlayMode]
    [BoxGroup("Is this object interactable at the start of the level?")]
    private bool _isInteractable = true;
    public bool IsInteractable => _isInteractable;
    
    [Min(0)][BoxGroup("How many times can the player interact with the object? 0 means infinite")]
    [SerializeField] private int _maxInteractionCount = 1; //0 means infinite
    
    [Range(0f, 1f)][ReadOnly]
    public float interactionProgress;
    
    [HideInInspector]
    public float totalHoverTime;
    
    private bool _isBeingHovered = false;
    private bool _hadInteractionSinceBeginHover = false;
    private bool _hasReachedMaxActivations = false;

    private int _totalInteractions = 0;

    [SerializeField]
    private UnityEvent onBeginHover;

    [SerializeField]
    private UnityEvent onEndHover;
    
    [SerializeField]
    private UnityEvent<int> onInteract; //passes along the total amount of interactions

    [SerializeField]
    private UnityEvent onBecameInteractable;
    
    [SerializeField]
    private UnityEvent onBecameNotInteractable;

    private void Awake()
    {
        if(!_isInteractable)
            onBecameNotInteractable.Invoke();
        else
            onBecameInteractable.Invoke();
    }

    public void BeginHover()
    {
        if (!_isInteractable)
            return;

        if (_hasReachedMaxActivations)
            return;
        
        if (_isBeingHovered)
            return;

        Debug.Log($"Begin hovering over game object: {gameObject.name}");
        _isBeingHovered = true;
        onBeginHover.Invoke();
    }

    public void EndHover()
    {
        if (!_isBeingHovered)
            return;
        
        Debug.Log($"Stopped hovering over game object: {gameObject.name}");

        interactionProgress = 0f;
        totalHoverTime = 0f;
        _isBeingHovered = false;
        _hadInteractionSinceBeginHover = false;
        onEndHover.Invoke();
    }

    public void Interact()
    {
        if (!_isInteractable)
            return;

        if (_hasReachedMaxActivations)
            return;
        
        if (_hadInteractionSinceBeginHover)
            return;
        
        Debug.Log($"Interact with game object: {gameObject.name}");
        
        _totalInteractions += 1;
        if (_maxInteractionCount != 0 && _totalInteractions >= _maxInteractionCount)
        {
            _hasReachedMaxActivations = true;
            SetIsInteractable(false);
        }
        _hadInteractionSinceBeginHover = true;
        onInteract.Invoke(_totalInteractions);
    }

    public void SetIsInteractable(bool isInteractable)
    {
        if (isInteractable && !_hasReachedMaxActivations)
        {
            _isInteractable = true;
            onBecameInteractable.Invoke();
            return;
        }

        _isInteractable = false;
        onBecameNotInteractable.Invoke();
    }
}