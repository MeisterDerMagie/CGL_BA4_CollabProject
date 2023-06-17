using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
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

    private float _currentSpeed = 0;

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

    [HideInInspector]
    public bool glueToSplines = true;
    private float GlueIntensity => 50f;

    private SplineContainer _splineContainer;
    private float _distanceToAttractRepelObject;

    private void Awake() => _mainCamera = Camera.main;


    public void ExecuteAttractOrRepel(Vector3 gazePoint)
    {
        if (!IsEnabled)
        {
            return;
        }

        AttractOrRepelPlayer attractOrRepelPlayer = RaycastUtility.ScreenPointRaycast<AttractOrRepelPlayer>(_mainCamera, gazePoint, RaycastUtility.GetComponentIn.Parent);

        if (attractOrRepelPlayer == null) return;

        //update direction
        Mode mode = attractOrRepelPlayer.Mode;
        
        _direction = Vector3.zero;
        if(mode == Mode.Repel) _direction = _characterController.transform.position - attractOrRepelPlayer.transform.position;
        else if (mode == Mode.Attract) _direction = attractOrRepelPlayer.transform.position - _characterController.transform.position;

        _direction = Vector3.Normalize(_direction);

        if (glueToSplines)
        {
            _splineContainer = attractOrRepelPlayer.GetComponentInChildren<AttractRepelPath>(true)?.SplineContainer;
            var objectCollider = attractOrRepelPlayer.GetComponentInChildren<Collider>();
            _distanceToAttractRepelObject = Vector3.Distance(_characterController.transform.position, objectCollider.transform.position);
        }
        
        //set isAttractingOrRepelling
        _isAttractingOrRepelling = true;
    }
    
    private void Update()
    {
        if (!IsEnabled) return;

        //if looking at an attract or repel object: move player
        Vector3 velocity = _currentVelocity;
        
        if (_isAttractingOrRepelling)
        {
            //move
            _currentVelocity += _direction * (acceleration * Time.deltaTime);
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, MaxSpeed);
            velocity = _currentVelocity;
            
            if (_splineContainer != null)
            {
                //get a point on the spline that's a bit ahead of the player and move the player towards this point
                Vector3 velocityTargetPoint = _characterController.transform.position + (_currentVelocity * GlueIntensity);
                Vector3 worldPositionOfSplineObject = _splineContainer.transform.position;
                float distance = SplineUtility.GetNearestPoint(_splineContainer.Spline, (float3)((velocityTargetPoint - worldPositionOfSplineObject)), out float3 nearestPointOnSpline, out float normalizedInterpolationRatio);

                nearestPointOnSpline += (float3)worldPositionOfSplineObject;

                Vector3 directionTowardsPointOnSpline = Vector3.Normalize((Vector3)nearestPointOnSpline - _characterController.transform.position);
                Vector3 velocityTowardsPointOnSpline = directionTowardsPointOnSpline * _currentVelocity.magnitude;
                
                
                Debug.DrawLine(_characterController.transform.position, (Vector3)velocityTargetPoint, Color.blue);
                Debug.DrawLine(velocityTargetPoint, (Vector3)nearestPointOnSpline, Color.blue);
                Debug.DrawLine(_characterController.transform.position, (Vector3)nearestPointOnSpline, Color.cyan);

                //stop movement when the player reached the end of the spline
                if (Vector3.Distance(_characterController.transform.position, (Vector3)nearestPointOnSpline) < 0.05f && _distanceToAttractRepelObject > 2f)
                    velocityTowardsPointOnSpline = Vector3.zero;
                
                velocity = velocityTowardsPointOnSpline;
            }

            //variables for stopping
            _decelerationProgress = MathW.Remap(_currentVelocity.magnitude, 0f, MaxSpeed, decelerationDuration, 0f);
            _forceWhenStopping = velocity;
        }

        //if not looking at an attract or repel object: decelerate
        else if(velocity.magnitude > 0f)
        {
            //decelerate
            _decelerationProgress = Mathf.Min(_decelerationProgress + Time.deltaTime, decelerationDuration);
            _decelerationProgressNormalized = MathW.Remap(_decelerationProgress, 0f, decelerationDuration, 0f, 1f);

            _currentVelocity = Vector3.Lerp(_forceWhenStopping, Vector3.zero, _decelerationProgressNormalized);
            velocity = _currentVelocity;
        }

        //draw velocity debug line
        Debug.DrawLine(_characterController.transform.position, (_characterController.transform.position + _currentVelocity * 100f), Color.yellow);
        
        //move character
        _characterController.Move(velocity);
        
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