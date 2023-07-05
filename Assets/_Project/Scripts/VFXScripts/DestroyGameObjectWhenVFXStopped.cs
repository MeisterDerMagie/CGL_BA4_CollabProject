//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class DestroyGameObjectWhenVFXStopped : MonoBehaviour
{
    private VisualEffect _visualEffect;

    private void Start()
    {
        _visualEffect = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        bool hasAnySystemAwake = _visualEffect.HasAnySystemAwake();
        if (!hasAnySystemAwake)
            Destroy(gameObject);
    }
}