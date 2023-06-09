using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : FirstPersonModule
{
    Rigidbody rb;
    CharacterController cc;

    public float movementSpeed;

    Vector3 movementVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    public void ExecuteWalk(Vector3 movementDirection)
    {
        if (!IsEnabled)
        {
            Debug.Log("Look around is not enabled.");
            return;
        }

        movementVector = transform.forward * movementDirection.z + transform.right * movementDirection.x;
        if (movementVector.magnitude > 1)
            movementVector = movementVector.normalized;

        movementVector = movementVector * Time.deltaTime * movementSpeed;
        cc.Move(movementVector);
    }
}
