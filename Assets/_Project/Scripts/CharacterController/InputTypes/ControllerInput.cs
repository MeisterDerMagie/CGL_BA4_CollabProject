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

    public ControllerInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        //Walk
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        movementDirection = new Vector3(moveX, 0, moveZ);
        FirstPersonController.GetModule<Walk>()?.ExecuteWalk(movementDirection);


        //Look Around
        FirstPersonController.GetModule<LookAround>()?.ExecuteLookAround(Input.mousePosition);

        //Teleport
        if (Input.GetKeyDown(KeyCode.Space))
            FirstPersonController.GetModule<Teleport>()?.ExecuteTeleport(Vector3.zero);
        
        
        //Hover and Interact
        FirstPersonController.GetModule<Hover>()?.ExecuteHover(Input.mousePosition);
        FirstPersonController.GetModule<Interact>()?.ExecuteInteract(Input.mousePosition);
    }
}