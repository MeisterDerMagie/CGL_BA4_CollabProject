using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[DisallowMultipleComponent]
public class Focus : FirstPersonModule
{
    private Focusable _previouslyFocusedObject;
    private Camera _mainCamera;

    private void Awake() => _mainCamera = Camera.main;

    public void ExecuteFocus(Vector3 gazePoint)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Focus is not enabled.");
            return;
        }

        Focusable focusable = RaycastUtility.ScreenPointRaycast<Focusable>(_mainCamera, gazePoint);

        //End Hover
        if (focusable == null || focusable != _previouslyFocusedObject)
        {
            if (_previouslyFocusedObject != null)
            {
                _previouslyFocusedObject.EndFocus();
                _previouslyFocusedObject = null;
            }
        }
        
        //do nothing if the focusable is an interactable. It will be taken care of in the Interact module.
        if (focusable is Interactable)
            return;
        
        //do nothing if the object has no focusable component or if the component is disabled
        if (focusable == null) return;
        if (!focusable.IsEnabled) return;
        
        //Focus or BeginFocus
        //if the object is newly being focused
        if (focusable != _previouslyFocusedObject)
        {
            focusable.BeginFocus();
            _previouslyFocusedObject = focusable;
        }
    }
}