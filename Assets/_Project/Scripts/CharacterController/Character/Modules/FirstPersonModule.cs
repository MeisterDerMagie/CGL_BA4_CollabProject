//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
public abstract class FirstPersonModule : MonoBehaviour
{
    [field: BoxGroup("Enable or disable this module")]
    [field: SerializeField]
    public bool IsEnabled { get; private set; }
    
    public void SetEnabled(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}