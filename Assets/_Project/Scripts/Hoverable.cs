//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Hoverable : MonoBehaviour
{
    [SerializeField][DisableInPlayMode][PropertyOrder(0)]
    [BoxGroup("Is this object interactable at the start of the level?")]
    protected bool _isEnabled = true;
    public bool IsEnabled => _isEnabled;
    
    [HideInInspector]
    public float totalHoverTime;
    
    protected bool _isBeingHovered = false;
    
    [PropertyOrder(20)]
    [SerializeField]
    protected UnityEvent onBeginHover;

    [PropertyOrder(30)]
    [SerializeField]
    protected UnityEvent onEndHover;
    
    [PropertyOrder(40)]
    [SerializeField]
    protected UnityEvent onBecameEnabled;
    
    [PropertyOrder(50)]
    [SerializeField]
    protected UnityEvent onBecameDisabled;
    
    private void Awake()
    {
        if(!_isEnabled)
            onBecameDisabled.Invoke();
        else
            onBecameEnabled.Invoke();
    }
    
    public virtual void BeginHover()
    {
        if (!_isEnabled)
            return;
        
        if (_isBeingHovered)
            return;

        //Debug.Log($"Begin hovering over game object: {gameObject.name}");
        _isBeingHovered = true;
        onBeginHover.Invoke();
    }

    public virtual void EndHover()
    {
        if (!_isBeingHovered)
            return;
        
        //Debug.Log($"Stopped hovering over game object: {gameObject.name}");
        
        totalHoverTime = 0f;
        _isBeingHovered = false;
        onEndHover.Invoke();
    }
    
    public virtual void SetEnabled(bool isEnabled)
    {
        _isEnabled = isEnabled;
        
        if(_isEnabled)
            onBecameEnabled.Invoke();
        else
            onBecameDisabled.Invoke();
    }
}