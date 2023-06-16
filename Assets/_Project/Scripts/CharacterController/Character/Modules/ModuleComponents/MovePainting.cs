using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePainting : FirstPersonModule
{
    [SerializeField]
    Painting leftPainting, rightPainting;

    [SerializeField]
    float movementSpeed;

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
}
