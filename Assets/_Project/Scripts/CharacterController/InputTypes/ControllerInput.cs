//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : InputType
{
    public ControllerInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        Debug.Log("Controller Input Tick.");
        
        if(Input.GetKeyDown(KeyCode.Space))
            FirstPersonController.GetModule<Teleport>()?.ExecuteTeleport(Vector3.zero);
    }
}