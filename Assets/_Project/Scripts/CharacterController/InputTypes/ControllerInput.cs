//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : InputType
{
    float controllerRotationX, controllerRotationY;
    float controllerDeadzone;

    public ControllerInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        controllerRotationX = Input.GetAxis("Mouse X");
        controllerRotationY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(controllerRotationX) > controllerDeadzone || Mathf.Abs(controllerRotationY) > controllerDeadzone)
        {
            FirstPersonController.GetModule<LookAround>()?.ExecuteLookAround(new Vector2(controllerRotationX, controllerRotationY));
        }

        Debug.Log("Controller Input Tick.");

        if(Input.GetKeyDown(KeyCode.Space))
            FirstPersonController.GetModule<Teleport>()?.ExecuteTeleport(Vector3.zero);
    }
}