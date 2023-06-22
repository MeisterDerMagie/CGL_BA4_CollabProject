//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Star : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onShowStar; //give the artists the possibility to do some fancy fade-in or what ever when the star becomes activated

    private void Start()
    {
        _onShowStar.Invoke();
    }
}