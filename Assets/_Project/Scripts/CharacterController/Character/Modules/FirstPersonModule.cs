//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
[DisallowMultipleComponent]
public abstract class FirstPersonModule : MonoBehaviour
{
    [BoxGroup("Enable or disable this module")]
    [SerializeField]
    private bool _isEnabled = true;
    public bool IsEnabled => _isEnabled;

    public virtual List<Type> IncompatibleModules => new List<Type>();

    public void SetEnabled(bool isEnabled)
    {
        _isEnabled = isEnabled;
    }
}