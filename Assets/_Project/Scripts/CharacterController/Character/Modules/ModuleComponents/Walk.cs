using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : FirstPersonModule
{
    Rigidbody rb;
    CharacterController cc;

    public float movementSpeed;
    public float gravity = -12;
    
    private Vector3 movementVector;
    private float velocityY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    public void ExecuteWalk(Vector3 movementDirection)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Look around is not enabled.");
            return;
        }
        
        velocityY += Time.deltaTime * gravity;

        movementVector = transform.forward * movementDirection.z + transform.right * movementDirection.x + Vector3.up * velocityY;
        if (movementVector.magnitude > 1)
            movementVector = movementVector.normalized;

        movementVector *= (Time.deltaTime * movementSpeed);
        cc.Move(movementVector);
        
        if (cc.isGrounded)
        {
            velocityY = 0;
        }
    }
}
