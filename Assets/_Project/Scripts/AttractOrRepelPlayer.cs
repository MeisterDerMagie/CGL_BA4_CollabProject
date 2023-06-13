//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttractOrRepelPlayer : MonoBehaviour
{
    [SerializeField][DisableInPlayMode]
    private AttractAndRepel.Mode mode;
    public AttractAndRepel.Mode Mode => mode;
    
    [BoxGroup("Override the default force")] [DisableInPlayMode]
    [SerializeField]
    private bool _overrideDefaultForce;
    public bool OverrideDefaultForce => _overrideDefaultForce;

    [BoxGroup("Override the default force")] [DisableInPlayMode]
    [ShowIf(nameof(_overrideDefaultForce))]
    [Min(0.1f)]
    [SerializeField]
    private float _customMaxForce = 1f;
    public float CustomMaxForce => _customMaxForce;
}