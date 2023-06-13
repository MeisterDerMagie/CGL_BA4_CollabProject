using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
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
    private float maxForceDesignerFriendly = 15f;
    private float MaxForce => maxForceDesignerFriendly / 100f;

    [SerializeField][LabelText("Acceleration")]
    private float accelerationDesignerFriendly = 2f;
    private float acceleration => accelerationDesignerFriendly / 100f;
    
    [SerializeField]
    private float decelerationDuration = 0.5f;
    
    
    private Vector3 _currentForce;
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
        
        Ray ray = _mainCamera.ScreenPointToRay(gazePoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);
        AttractOrRepelPlayer attractOrRepelPlayer = hitAnObject ? hit.collider.GetComponent<AttractOrRepelPlayer>() : null;

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
            _currentForce += _direction * (acceleration * Time.deltaTime);
            _currentForce = Vector3.ClampMagnitude(_currentForce, MaxForce);
            
            //variables for stopping
            _decelerationProgress = MathW.Remap(_currentForce.magnitude, 0f, MaxForce, decelerationDuration, 0f);
            _forceWhenStopping = _currentForce;
        }

        else if(_currentForce.magnitude > 0f)
        {
            _decelerationProgress = Mathf.Min(_decelerationProgress + Time.deltaTime, decelerationDuration);
            _decelerationProgressNormalized = MathW.Remap(_decelerationProgress, 0f, decelerationDuration, 0f, 1f);
            
            _currentForce = Vector3.Lerp(_forceWhenStopping, Vector3.zero, _decelerationProgressNormalized);
        }
        
        Debug.DrawLine(transform.position, (transform.position + _currentForce * 100f), Color.yellow);
        
        //move character
        _characterController.Move(_currentForce);
        
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