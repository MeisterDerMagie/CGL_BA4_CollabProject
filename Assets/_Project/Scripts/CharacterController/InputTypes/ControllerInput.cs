//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : InputType
{
    //Movement Variables
    float moveX, moveZ;
    Vector3 movementDirection;

    //Rotation Variables
    float controllerRotationX, controllerRotationY;
    float controllerDeadzone;

    public ControllerInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        Debug.Log("Controller Input Tick.");

        //Walk
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        movementDirection = new Vector3(moveX, 0, moveZ);
        FirstPersonController.GetModule<Walk>()?.ExecuteWalk(movementDirection);


        //Look Around
        controllerRotationX = Input.GetAxis("Mouse X");
        controllerRotationY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(controllerRotationX) > controllerDeadzone || Mathf.Abs(controllerRotationY) > controllerDeadzone)
        {
            FirstPersonController.GetModule<LookAround>()?.ExecuteLookAround(new Vector2(controllerRotationX, controllerRotationY));
        }

        //Teleport
        if (Input.GetKeyDown(KeyCode.Space))
            FirstPersonController.GetModule<Teleport>()?.ExecuteTeleport(Vector3.zero);
        
        
        //Interact
        FirstPersonController.GetModule<Interact>()?.ExecuteInteract(Input.mousePosition);
    }
}