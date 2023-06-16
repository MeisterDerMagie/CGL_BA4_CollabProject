using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePainting : FirstPersonModule
{
    [SerializeField]
    Painting leftPainting, rightPainting;

    [SerializeField]
    float movementSpeed;

    Vector3 startPosRightPainting, startPosLeftPainting;

    private void Awake()
    {
        onEnabledChanged += SetStartPositions;
        
        if(IsEnabled)
            SetStartPositions(true);
    }

    private void OnDestroy() => onEnabledChanged -= SetStartPositions;

    public void ExecuteMovePainting(float xMovement)
    {
        if (!IsEnabled) return;

        if (xMovement > 0.5f)
        {
            leftPainting.Move(new Vector3(movementSpeed, 0, 0));
            rightPainting.ResetPosition();
        }
        else if (xMovement < -0.5f)
        {
            rightPainting.Move(new Vector3(-movementSpeed, 0, 0));
            leftPainting.ResetPosition();
        }
        else
        {
            leftPainting.StopMoving();
            rightPainting.StopMoving();
        }
    }

    private void SetStartPositions(bool isEnabled)
    {
        //set start positions when the module is enabled
        if (!isEnabled) return;
        
        startPosLeftPainting = leftPainting.transform.position;
        startPosRightPainting = rightPainting.transform.position;
    }
}
