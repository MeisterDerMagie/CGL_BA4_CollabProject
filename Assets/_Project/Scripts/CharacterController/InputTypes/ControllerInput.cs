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
        FirstPersonController.GetModule<LookAround>()?.ExecuteLookAroundMouse(Input.mousePosition);

        //Teleport
        if (Input.GetKeyDown(KeyCode.Space))
            FirstPersonController.GetModule<Teleport>()?.ExecuteTeleport();
        
        //Focus and Interact
        FirstPersonController.GetModule<Focus>()?.ExecuteFocus(Input.mousePosition);
        FirstPersonController.GetModule<Interact>()?.ExecuteInteract(Input.mousePosition);
        
        //Move on Rails (Player Controlled)
        FirstPersonController.GetModule<MoveOnRailsPlayerControlled>()?.ExecuteMovement(Input.GetAxis("Vertical"));

        //Pop bubbles
        BubbleBehavior bubble = RaycastUtility.ScreenPointRaycast<BubbleBehavior>(Camera.main, Input.mousePosition);
        if (bubble != null && Input.GetKeyDown(KeyCode.E))
            FirstPersonController.GetModule<PopBubbles>()?.ExecutePopBubbles(bubble.gameObject);
        
        //Push and pull
        FirstPersonController.GetModule<AttractAndRepel>()?.ExecuteAttractOrRepel(Input.mousePosition);
    }
}