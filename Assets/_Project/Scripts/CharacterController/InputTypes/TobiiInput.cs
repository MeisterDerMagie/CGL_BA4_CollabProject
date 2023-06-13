//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TobiiInput : InputType
{
    Vector2 gazePoint;

    Vector3 headPos;

    Quaternion headRotation;

    public TobiiInput(FirstPersonController firstPersonController) : base(firstPersonController)
    {
    }

    public override void Tick()
    {
        //Return if no user is present
        if (!TobiiAPI.GetUserPresence().IsUserPresent()) return;

        //Get gaze point as Vector2
        if (TobiiAPI.GetGazePoint().IsRecent())
            gazePoint = TobiiAPI.GetGazePoint().Screen;

        //Get head position and rotation
        if (TobiiAPI.GetHeadPose().IsRecent())
        {
            headPos = TobiiAPI.GetHeadPose().Position;
            headRotation = TobiiAPI.GetHeadPose().Rotation;
        }

        //Look Around
        FirstPersonController.GetModule<LookAround>()?.ExecuteLookAroundTobii(headRotation);

        //Interact
        FirstPersonController.GetModule<Focus>()?.ExecuteFocus(gazePoint);
        FirstPersonController.GetModule<Interact>()?.ExecuteInteract(gazePoint);

        //Pop Bubbles
        FirstPersonController.GetModule<PopBubbles>()?.ExecutePopBubbles(headPos.z);
        
        //Push and pull
        FirstPersonController.GetModule<AttractAndRepel>()?.ExecuteAttractOrRepel(gazePoint);

        Debug.Log("TobiiInput tick.");
    }
}