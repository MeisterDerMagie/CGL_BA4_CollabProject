//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
[HideMonoScript]
public abstract class FirstPersonModule : MonoBehaviour
{
    
    [HorizontalGroup("Enable or disable this module")]
    [Button(Name = "Enable Module")][PropertyOrder(-1)][DisableIf(nameof(_isEnabled))]
    private void EnableModuleFromInspector()
    {
        GetComponent<FirstPersonController>()?.DisableIncompatibleModules(this);
        SetEnabled(true);
    }

    [HorizontalGroup("Enable or disable this module")]
    [Button(Name = "Disable Module")][PropertyOrder(-1)][EnableIf(nameof(_isEnabled))]
    private void DisableModuleFromInspector() => SetEnabled(false);
    
    
    [InfoBox("This module is enabled.", InfoMessageType.Info, nameof(_isEnabled), GUIAlwaysEnabled = true)]
    [InfoBox("This module is disabled.", InfoMessageType.Info, nameof(IsEnabledInverted), GUIAlwaysEnabled = true)]
    [SerializeField][ReadOnly][DisplayAsString][GUIColor(nameof(GetEnabledColor))]
    private bool _isEnabled = false;
    public bool IsEnabled => _isEnabled;
    private bool IsEnabledInverted => !_isEnabled;
    private Color GetEnabledColor => _isEnabled ? Color.green : new Color(1f, 0.87f, 0.72f, 1f);
    
    protected event Action<bool> onEnabledChanged = delegate(bool isEnabled) {  }; 

    public virtual List<Type> IncompatibleModules => new List<Type>();

    public void SetEnabled(bool isEnabled)
    {
        _isEnabled = isEnabled;
        onEnabledChanged?.Invoke(_isEnabled);
    }
}