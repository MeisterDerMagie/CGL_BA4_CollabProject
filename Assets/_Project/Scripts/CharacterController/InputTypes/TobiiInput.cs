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
    float lastXPos;

    float rotateToPop, startValue;

    bool popable;

    public TobiiInput(FirstPersonController firstPersonController, float _backValue, float moveToPop, float _rotateToPop, float _startValue) : base(firstPersonController)
    {
        backValue = _backValue;
        popValue = backValue + (moveToPop / 100);
        rotateToPop = _rotateToPop;
        startValue = _startValue;
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
        //Popping with moving head forward
        /*if (Mathf.Abs(headPos.z) >= backValue)
        {
            Debug.Log("Back Value has been reached");
            popable = true;
        }
        GameObject bubble = TobiiAPI.GetFocusedObject();
        if (null != bubble)
        {
            Debug.Log(bubble.name);
            if (Mathf.Abs(headPos.z) <= popValue && popable == true)
            {
                Debug.Log("Pop Value has been reached");
                FirstPersonController.GetModule<PopBubbles>()?.ExecutePopBubbles(bubble);
                popable = false;
            }
        }*/

        //Popping with nodding
        if (headRotation.eulerAngles.x <= startValue)
        {
            Debug.Log("Back Value has been reached");
            popable = true;
        }
        GameObject Bubble = TobiiAPI.GetFocusedObject();

        if (Bubble != null)
        {
            Debug.Log(Bubble.name);
            if (headRotation.eulerAngles.x >= rotateToPop && popable == true) // Clamp value to 300 -> wenn man zu weit hochstreckt ist der wert bei 360 und damit auch über der abfrage value
            {
                Debug.Log("Pop Value has been reached");
                popable = false;
                FirstPersonController.GetModule<PopBubbles>()?.ExecutePopBubbles(Bubble);
                Bubble = null;
            }
        }

        //Push and pull
        FirstPersonController.GetModule<AttractAndRepel>()?.ExecuteAttractOrRepel(gazePoint);

        //Move Painting
        float headX = Mathf.Round(headPos.x * 100) / 100;
        //Debug.Log(headX);
        if (headX > lastXPos)
        {
            FirstPersonController.GetModule<MovePainting>()?.ExecuteMovePainting(1);
            lastXPos = headX;
        }
        else if (headX < lastXPos)
        {
            FirstPersonController.GetModule<MovePainting>()?.ExecuteMovePainting(-1);
            lastXPos = headX;
        }
        else
            FirstPersonController.GetModule<MovePainting>()?.ExecuteMovePainting(0);

        Debug.Log("TobiiInput tick.");
    }
}