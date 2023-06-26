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

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Painting")) return;

        //track the total time inside the trigger
        _triggerStayDuration += Time.deltaTime;
        if (_triggerStayDuration >= _secondsUntilActivation)
        {
            //load next scenen when the painting was in the middle for long enough
            _sceneFlow.PaintingActivated();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Painting")) return;

        //reset total trigger duration
        _triggerStayDuration = 0f;
    }
}
