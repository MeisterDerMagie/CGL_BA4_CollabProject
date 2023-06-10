using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : FirstPersonModule
{
    [SerializeField] private CharacterController characterController;

    public float movementSpeed;
    public float gravity = -12;
    
    private Vector3 movementVector;
    private float velocityY;

    public void ExecuteWalk(Vector3 movementDirection)
    {
        if (!IsEnabled)
            return;

        velocityY += Time.deltaTime * gravity;

        movementVector = transform.forward * movementDirection.z + transform.right * movementDirection.x + Vector3.up * velocityY;
        if (movementVector.magnitude > 1)
            movementVector = movementVector.normalized;

        movementVector *= (Time.deltaTime * movementSpeed);
        characterController.Move(movementVector);
        
        if (characterController.isGrounded)
        {
            velocityY = 0;
        }
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
    }
    #endif
}
