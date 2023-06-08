//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
public abstract class FirstPersonModule : MonoBehaviour
{
    [field: SerializeField]
    [field: HideInInspector]
    public bool IsEnabled { get; private set; }
    
    [Button]
    public void SetEnabled(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}