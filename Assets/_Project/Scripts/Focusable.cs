//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Focusable : MonoBehaviour
{
    [SerializeField][DisableInPlayMode][PropertyOrder(0)]
    [BoxGroup("Is this object focusable at the start of the level?")]
    protected bool _isEnabled = true;
    public bool IsEnabled => _isEnabled;
    
    [HideInInspector]
    public float totalFocusTime;
    
    protected bool _isBeingFocused = false;
    
    [PropertyOrder(20)]
    [SerializeField]
    protected UnityEvent onBeginFocus;
    
    [PropertyOrder(30)]
    [SerializeField]
    protected UnityEvent onEndFocus;
    
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
    
    public virtual void BeginFocus()
    {
        if (!_isEnabled)
            return;
        
        if (_isBeingFocused)
            return;

        //Debug.Log($"Begin focussing game object: {gameObject.name}");
        _isBeingFocused = true;
        onBeginFocus.Invoke();
    }

    public virtual void EndFocus()
    {
        if (!_isBeingFocused)
            return;
        
        //Debug.Log($"Stopped focussing game object: {gameObject.name}");
        
        totalFocusTime = 0f;
        _isBeingFocused = false;
        onEndFocus.Invoke();
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