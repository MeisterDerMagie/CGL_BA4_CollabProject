//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TobiiInput : InputType
{
    public TobiiInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        Debug.Log("TobiiInput tick.");
    }
}