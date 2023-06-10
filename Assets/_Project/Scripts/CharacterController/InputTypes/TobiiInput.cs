//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TobiiInput : InputType
{
    Vector2 gazePoint;

    public TobiiInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        //Look Around
        gazePoint = TobiiAPI.GetGazePoint().Screen;
        FirstPersonController.GetModule<LookAround>().ExecuteLookAround(gazePoint);

        Debug.Log("TobiiInput tick.");
    }
}