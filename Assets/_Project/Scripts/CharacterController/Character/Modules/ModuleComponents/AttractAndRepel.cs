using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Wichtel;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
//https://forum.unity.com/threads/how-to-multiply-vector3-towards-zero-considering-time-deltatime.1448338/
public class AttractAndRepel : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(MoveOnRailsAnimated),
            typeof(MoveOnRailsPlayerControlled),
            typeof(MoveOnRailsScripted)
        };
    
    public enum Mode
    {
        Attract,
        Repel
    }
    
    [SerializeField][HideInInspector]
    private CharacterController _characterController;

    private Camera _mainCamera;
    
    [SerializeField][LabelText("Max Force")]
    private float maxSpeedDesignerFriendly = 15f;
    private float MaxSpeed => maxSpeedDesignerFriendly / 100f;

    [SerializeField][LabelText("Acceleration")]
    private float accelerationDesignerFriendly = 2f;
    private float acceleration => accelerationDesignerFriendly / 100f;
    
    [SerializeField]
    private float decelerationDuration = 0.5f;
    
    
    private Vector3 _currentVelocity;
    private Vector3 _direction;
    private bool _isAttractingOrRepelling;
    private float _decelerationProgress;
    private float _decelerationProgressNormalized;
    private Vector3 _forceWhenStopping;
    
    private void Awake() => _mainCamera = Camera.main;

    
    public void ExecuteAttractOrRepel(Vector3 gazePoint)
    {
        if (!IsEnabled)
        {
            return;
        }

        AttractOrRepelPlayer attractOrRepelPlayer = RaycastUtility.ScreenPointRaycast<AttractOrRepelPlayer>(_mainCamera, gazePoint);

        if (attractOrRepelPlayer == null) return;

        //update direction
        Mode mode = attractOrRepelPlayer.Mode;
        
        _direction = Vector3.zero;
        if(mode == Mode.Attract) _direction = _characterController.transform.position - attractOrRepelPlayer.transform.position;
        else if (mode == Mode.Repel) _direction = attractOrRepelPlayer.transform.position - _characterController.transform.position;
        
        //set isAttractingOrRepelling
        _isAttractingOrRepelling = true;
    }
    
    private void Update()
    {
        if (!IsEnabled) return;

        if (_isAttractingOrRepelling)
        {
            //move
            _currentVelocity += _direction * (acceleration * Time.deltaTime);
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, MaxSpeed);
            
            //variables for stopping
            _decelerationProgress = MathW.Remap(_currentVelocity.magnitude, 0f, MaxSpeed, decelerationDuration, 0f);
            _forceWhenStopping = _currentVelocity;
        }

        else if(_currentVelocity.magnitude > 0f)
        {
            _decelerationProgress = Mathf.Min(_decelerationProgress + Time.deltaTime, decelerationDuration);
            _decelerationProgressNormalized = MathW.Remap(_decelerationProgress, 0f, decelerationDuration, 0f, 1f);
            
            _currentVelocity = Vector3.Lerp(_forceWhenStopping, Vector3.zero, _decelerationProgressNormalized);
        }
        
        Debug.DrawLine(transform.position, (transform.position + _currentVelocity * 100f), Color.yellow);
        
        //move character
        _characterController.Move(_currentVelocity);
        
        //reset isAttractingOrRepelling
        _isAttractingOrRepelling = false;
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
        if(_characterController == null) Debug.LogError("PushAndPull module needs a characterController!");
    }
    #endif
}