using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : FirstPersonModule
{
    CharacterController cc;

    public float movementSpeed;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public void ExecuteWalk(Vector3 movementDirection)
    {
        if (!IsEnabled)
        {
            Debug.Log("Look around is not enabled.");
            return;
        }

        movementDirection = movementDirection * Time.deltaTime * movementSpeed;
        cc.Move(movementDirection);
    }
}
