//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubStars : MonoBehaviour
{
    private void Awake()
    {
        //disable this object when the scene is loaded. it will be activated later when the player looks at the iris
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}