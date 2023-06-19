//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Wichtel.Extensions;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class LockViewOnTarget : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(LookAround),
        };
    
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _fieldOfView = 60f;

    [SerializeField]
    private float _inertia = 4f;

    private CharacterController _characterController;
    private Camera _mainCamera;
    private Quaternion _initialRotation;
    private float _initialFieldOfView;
    private float _transitionProgress = 0f;
    private TweenerCore<float, float, FloatOptions> _transitionTween;
    
    private void Start()
    {
        onEnabledChanged += OnIsEnabledChanged;
        
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        
        ResetValues();
        
        if (IsEnabled) StartTransition();
    }

    private void OnDestroy() => onEnabledChanged -= OnIsEnabledChanged;

    public void ChangeTargetAndFieldOfView(Transform target, float fieldOfView)
    {
        _target = target;
        
        ResetValues(fieldOfView);
        StartTransition();
    }

    public void ChangeTarget(Transform target)
    {
        _target = target;
        
        ResetValues();
        StartTransition();
    }

    public void ChangeFieldOfView(float fieldOfView)
    {
        ResetValues(fieldOfView);
        StartTransition();
    }

    private void Update()
    {
        if (!IsEnabled) return;
        if(_target == null) return;
        
        //lock on target
        Vector3 direction = _target.position - _mainCamera.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
        
        Quaternion newRotation = Quaternion.Lerp(_initialRotation, toRotation, _transitionProgress);
        
        //rotate camera on x axis
        _mainCamera.transform.rotation = Quaternion.Euler(_mainCamera.transform.rotation.eulerAngles.With(x: newRotation.eulerAngles.x));
        
        //rotate character controller on y axis
        _characterController.transform.rotation = Quaternion.Euler(_characterController.transform.rotation.eulerAngles.With(y: newRotation.eulerAngles.y));
        
        //keep z rotation of camera at 0
        _mainCamera.transform.rotation = Quaternion.Euler(_mainCamera.transform.rotation.eulerAngles.With(z: 0f));
        
        // zoom
        _mainCamera.fieldOfView = Mathf.Lerp(_initialFieldOfView, _fieldOfView, _transitionProgress);
    }

    private void ResetValues(float fieldOfView = float.PositiveInfinity)
    {
        _initialRotation = _mainCamera.transform.rotation;
        _transitionProgress = 0f;
        _initialFieldOfView = _mainCamera.fieldOfView;

        if (fieldOfView < float.PositiveInfinity)
        {
            _fieldOfView = fieldOfView;
        }
    }

    //smoothly tween to the target rotation
    private void StartTransition()
    {
        if (_transitionTween != null && _transitionTween.IsActive()) _transitionTween.Kill();
        
        //panning
        Vector3 direction = _target.position - _mainCamera.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
        float angleDifference = Quaternion.Angle(_initialRotation, toRotation);
        float duration = 3f + _inertia * angleDifference * 0.005f;
        _transitionTween = DOTween.To(() => _transitionProgress, x => _transitionProgress = x, 1f, duration).SetEase(Ease.InOutCubic);
    }

    private void OnIsEnabledChanged(bool isEnabled)
    {
        if (!isEnabled) return;
        ResetValues();
        StartTransition();
    }
}