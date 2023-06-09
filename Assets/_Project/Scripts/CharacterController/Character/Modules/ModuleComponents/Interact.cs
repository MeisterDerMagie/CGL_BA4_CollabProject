using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Interact : FirstPersonModule
{
    [field: SerializeField] [field: BoxGroup("How many seconds you need to look at an object until you interact with it")]
    public float InteractionDurationSeconds { get; private set; } = 3f;
    
    private Interactable _previouslyHoveredObject;
    private Camera _mainCamera;

    private void Awake() => _mainCamera = Camera.main;

    public void ExecuteInteract(Vector3 gazePoint)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Interact is not enabled.");
            return;
        }
        
        Ray ray = _mainCamera.ScreenPointToRay(gazePoint);
        bool hitAnObject = Physics.Raycast(ray, out RaycastHit hit);
        Interactable interactable = hitAnObject ? hit.collider.GetComponent<Interactable>() : null;

        //End Hover
        if (interactable == null || interactable != _previouslyHoveredObject)
        {
            if (_previouslyHoveredObject != null)
            {
                _previouslyHoveredObject.EndHover();
                _previouslyHoveredObject = null;
            }
        }

        //do nothing if the object has no interactable component or if the component is disabled
        if (interactable == null) return;
        if (!interactable.IsEnabled) return;
        
        //Hover or BeginHover
        //if the object is newly being hovered
        if (interactable != _previouslyHoveredObject)
        {
            interactable.BeginHover();
            _previouslyHoveredObject = interactable;
        }
                
        //if hovering the same object like last frame
        //Interact Progress
        else
        {
            interactable.totalHoverTime += Time.deltaTime;
            float interactionProgress = 1f / InteractionDurationSeconds * _previouslyHoveredObject.totalHoverTime;
            interactable.interactionProgress = Mathf.Clamp(interactionProgress, 0f, 1f);
        }
                
        //Interact (progress complete)
        if (interactable.interactionProgress >= 1f)
        {
            interactable.Interact();
        }
    }
}