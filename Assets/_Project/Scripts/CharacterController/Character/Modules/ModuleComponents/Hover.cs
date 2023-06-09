using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Hover : FirstPersonModule
{
    private Hoverable _previouslyHoveredObject;
    private Camera _mainCamera;

    private void Awake() => _mainCamera = Camera.main;

    public void ExecuteHover(Vector3 gazePoint)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Hovering is not enabled.");
            return;
        }
        
        Ray ray = _mainCamera.ScreenPointToRay(gazePoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);
        Hoverable hoverable = hitAnObject ? hit.collider.GetComponent<Hoverable>() : null;

        //End Hover
        if (hoverable == null || hoverable != _previouslyHoveredObject)
        {
            if (_previouslyHoveredObject != null)
            {
                _previouslyHoveredObject.EndHover();
                _previouslyHoveredObject = null;
            }
        }
        
        //do nothing if the hoverable is an interactable. It will be taken care of in the Interact module.
        if (hoverable is Interactable)
            return;
        
        //do nothing if the object has no hoverable component or if the component is disabled
        if (hoverable == null) return;
        if (!hoverable.IsEnabled) return;
        
        //Hover or BeginHover
        //if the object is newly being hovered
        if (hoverable != _previouslyHoveredObject)
        {
            hoverable.BeginHover();
            _previouslyHoveredObject = hoverable;
        }
    }
}