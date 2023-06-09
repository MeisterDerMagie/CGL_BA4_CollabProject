using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[DisallowMultipleComponent]
public class Interact : FirstPersonModule
{
    [field: SerializeField] [field: BoxGroup("How many seconds you need to look at an object until you interact with it")]
    public float InteractionDurationSeconds { get; private set; } = 3f;
    
    private Interactable _previouslyFocusedObject;
    private Camera _mainCamera;

    private void Awake() => _mainCamera = Camera.main;

    public void ExecuteInteract(Vector3 gazePoint)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Interact is not enabled.");
            return;
        }

        Interactable interactable = RaycastUtility.ScreenPointRaycast<Interactable>(_mainCamera, gazePoint);

        //End Focus
        if (interactable == null || interactable != _previouslyFocusedObject)
        {
            if (_previouslyFocusedObject != null)
            {
                _previouslyFocusedObject.EndFocus();
                _previouslyFocusedObject = null;
            }
        }

        //do nothing if the object has no interactable component or if the component is disabled
        if (interactable == null) return;
        if (!interactable.IsEnabled) return;
        
        //Focus or BeginFocus
        //if the object is newly being focused
        if (interactable != _previouslyFocusedObject)
        {
            interactable.BeginFocus();
            _previouslyFocusedObject = interactable;
        }
                
        //if focusing the same object like last frame
        //Interact Progress
        else
        {
            interactable.totalFocusTime += Time.deltaTime;
            float interactionDuration = interactable.OverrideInteractionDuration ? interactable.CustomInteractionDuration : InteractionDurationSeconds;
            float interactionProgress = 1f / interactionDuration * interactable.totalFocusTime; //this works for interactionDuration == 0 because 1f / 0f returns Infinity instead of an error
            interactable.interactionProgress = Mathf.Clamp(interactionProgress, 0f, 1f);
        }
                
        //Interact (progress complete)
        if (interactable.interactionProgress >= 1f)
        {
            interactable.Interact();
        }
    }
}