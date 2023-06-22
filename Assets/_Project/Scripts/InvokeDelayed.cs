using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InvokeDelayed : MonoBehaviour
{
    [SerializeField]
    private float _delayInSeconds = 0f;

    [SerializeField]
    private UnityEvent _invokeDelayed;
    
    private void Start() => Timing.RunCoroutine(_InvokeDelayed());

    private IEnumerator<float> _InvokeDelayed()
    {
        yield return Timing.WaitForSeconds(_delayInSeconds);
        _invokeDelayed.Invoke();
    }
}
