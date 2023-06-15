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

    float backValue, popValue;

    bool popable;

    public TobiiInput(FirstPersonController firstPersonController, float _backValue, float _popValue) : base(firstPersonController)
    {
        backValue = _backValue;
        popValue = _popValue;
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
        if (headPos.z >= backValue) popable = true;

        GameObject bubble = TobiiAPI.GetFocusedObject();
        if (bubble != null)
        {
            if (headPos.z <= popValue && popable == true)
            {
                FirstPersonController.GetModule<PopBubbles>()?.ExecutePopBubbles(bubble);
                popable = false;
            }
        }
        
        //Push and pull
        FirstPersonController.GetModule<AttractAndRepel>()?.ExecuteAttractOrRepel(gazePoint);

        Debug.Log("TobiiInput tick.");
    }
}