//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Star : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onShowStar; //give the artists the possibility to do some fancy fade-in or what ever when the star becomes visible

    public void ShowStar()
    {
        _onShowStar.Invoke();
    }
}