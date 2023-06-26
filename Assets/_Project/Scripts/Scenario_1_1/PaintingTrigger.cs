using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
public class PaintingTrigger : MonoBehaviour
{
    [SerializeField]
    private float _secondsUntilActivation;

    [SerializeField]
    private SceneFlow_Scenario_1_1 _sceneFlow;
    
    private float _triggerStayDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Painting")) return;
        
        //call the on enter event on the painting
        other.GetComponent<Painting>().OnPaintingTriggerEnter.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Painting")) return;

        //track the total time inside the trigger
        _triggerStayDuration += Time.deltaTime;
        
        //call the trigger stay method on the painting
        float triggerStayDurationNormalized = Mathf.Clamp(1f / _secondsUntilActivation * _triggerStayDuration, 0f, 1f);
        other.GetComponent<Painting>().OnPaintingTriggerStay(triggerStayDurationNormalized);
        
        //load next scenen when the painting was in the middle for long enough
        if (_triggerStayDuration >= _secondsUntilActivation)
        {
            _sceneFlow.PaintingActivated();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Painting")) return;

        //call the on exit event on the painting
        other.GetComponent<Painting>().OnPaintingTriggerExit.Invoke();
        
        //reset total trigger duration
        _triggerStayDuration = 0f;
    }
}