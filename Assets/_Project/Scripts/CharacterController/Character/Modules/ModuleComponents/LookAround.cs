//(c) copyright by Martin M. Klöckener
using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LookAround : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(LockViewOnTarget),
        };
    
    public GameObject cam;

    float verticalRotation, horizontalRotation;

    [SerializeField]
    float rotationXBoundary, rotationYBoundary;

    public float minVerticalRotation, maxVerticalRotation, rotationSpeed;
    float RotationSpeed => rotationSpeed * 350;

    [Range(0, 50)]
    public float screenBoundaryPercentage;
    [Range(0, 5)]
    public float rotationSpeedIncreaseFactor;

    public void ExecuteLookAroundMouse(Vector3 mousePos)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Look around is not enabled.");
            return;
        }

        // Making sure that the mouse is not outside of the screen
        float mouseXPercentage = mousePos.x / Screen.width;
        float mouseYPercentage = mousePos.y / Screen.height;
        if (mouseXPercentage < 0 || mouseXPercentage > 1 || mouseYPercentage < 0 || mouseYPercentage > 1) return;

        // Calculation for X Rotation
        float mouseXSide = Mathf.Sign(mouseXPercentage - 0.5f);
        float mouseXCenterDistance = (mouseXPercentage - 0.5f) * mouseXSide;
        mouseXCenterDistance = mouseXCenterDistance - (0.5f - screenBoundaryPercentage / 100f);

        // Calculation for Y Rotation
        float mouseYSide = Mathf.Sign(mouseYPercentage - 0.5f);
        float mouseYCenterDistance = (mouseYPercentage - 0.5f) * mouseYSide;
        mouseYCenterDistance = mouseYCenterDistance - (0.5f - screenBoundaryPercentage / 100f);

        // Rotation on X-Axis
        if (mouseXCenterDistance > 0)
        {
            float currentXRotationSpeed = mouseXCenterDistance * Mathf.Pow(RotationSpeed, rotationSpeedIncreaseFactor) * mouseXSide * Time.deltaTime;
            transform.Rotate(Vector3.up, currentXRotationSpeed);
        }

        // Rotation on Y-Axis
        if (mouseYCenterDistance > 0)
        {
            float currentYRotationSpeed = mouseYCenterDistance * Mathf.Pow(RotationSpeed, rotationSpeedIncreaseFactor) * mouseYSide * Time.deltaTime;
            verticalRotation -= currentYRotationSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    public void ExecuteLookAroundTobii(Quaternion headRotation)
    {
        if (!IsEnabled) return;
        //Camera and player rotate with the same amount as the player's head
        //Maybe smoother?
        /*transform.rotation = Quaternion.Euler(0, headRotation.eulerAngles.y, 0);
        cam.transform.localRotation = Quaternion.Euler(headRotation.eulerAngles.x, 0, 0);*/

        // Character rotates as long as head of the player is rotated to the certain side
        if (Mathf.Abs(headRotation.y) > rotationYBoundary / 100)
        {
            Debug.Log("Rotating Player");
            float rotationSign = Mathf.Sign(headRotation.y);
            float rotationXSpeed = Mathf.Pow(Mathf.Abs(headRotation.y) * 10, rotationSpeedIncreaseFactor) * rotationSign * Time.deltaTime;
            horizontalRotation += rotationXSpeed;
            horizontalRotation = Mathf.Clamp(horizontalRotation, -360, 360);
            transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
            //transform.Rotate(Vector3.up, rotationXSpeed);
        }

        if (Mathf.Abs(headRotation.x) > rotationXBoundary / 100)
        {
            Debug.Log("Rotating Cam");
            float rotationSign = Mathf.Sign(headRotation.x);
            float rotationYSpeed = Mathf.Pow(Mathf.Abs(headRotation.x) * 10, rotationSpeedIncreaseFactor) * rotationSign * Time.deltaTime;
            verticalRotation += rotationYSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            //cam.transform.Rotate(Vector3.left, -rotationYSpeed);
        }

        //Debug.Log("Quaternion y: " + Mathf.Abs(headRotation.y));
    }
}