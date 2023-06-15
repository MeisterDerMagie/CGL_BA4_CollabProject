using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePainting : FirstPersonModule
{
    [SerializeField]
    GameObject leftPainting, rightPainting;

    [SerializeField]
    float movementSpeed;

    Vector3 startPosRightPainting, startPosLeftPainting;

    private void Awake()
    {
        startPosLeftPainting = leftPainting.transform.position;
        startPosRightPainting = rightPainting.transform.position;
    }

    public void ExecuteMovePainting(float xMovement)
    {
        if (!IsEnabled) return;

        if (xMovement > 0.5f)
        {
            leftPainting.transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            rightPainting.transform.position = startPosRightPainting;
        }
        else if (xMovement < -0.5f)
        {
            rightPainting.transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            Vector3 startPos = leftPainting.transform.position;
            leftPainting.transform.position = Vector3.Lerp(startPos, startPosLeftPainting, 4f);
        }
    }
}
