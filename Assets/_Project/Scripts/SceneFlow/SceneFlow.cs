//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public abstract class SceneFlow : MonoBehaviour
{
    protected CoroutineHandle Coroutine;
    
    private void Start()
    {
        Coroutine = Timing.RunCoroutine(_SceneFlow());
    }

    protected abstract IEnumerator<float> _SceneFlow();
}