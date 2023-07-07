//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ScaleUpWhenActivating : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    
    [SerializeField]
    private float _scaleFactor;
    
    private Interactable _interactable;
    private Vector3 _initialScale;
    
    private void Start()
    {
        _interactable = GetComponent<Interactable>();
        _initialScale = _target.localScale;
    }

    private void Update()
    {
        _target.localScale = _initialScale + Vector3.one * (_scaleFactor * _interactable.interactionProgress);
    }
}