//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public abstract class SceneFlow : MonoBehaviour
{
    private void Start()
    {
        Timing.RunCoroutine(_SceneFlow());
    }

    protected abstract IEnumerator<float> _SceneFlow();
}