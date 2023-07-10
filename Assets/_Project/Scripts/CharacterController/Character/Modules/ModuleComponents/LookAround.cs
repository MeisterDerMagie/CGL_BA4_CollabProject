//(c) copyright by Martin M. Klöckener
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tobii.Gaming;
using UnityEngine;

[DisallowMultipleComponent]
public class LookAround : FirstPersonModule
{
    public override List<Type> IncompatibleModules =>
        new ()
        {
            typeof(LockViewOnTarget),
        };

    public enum RotationType
    {
        Continuously,
        WithBoundary
    }

    [SerializeField]
    public RotationType rotationType;

    public GameObject cam;

    float verticalRotation, horizontalRotation;

    [SerializeField] [field: BoxGroup("If 100, Player needs to turn his head completely to the side to rotate")]
    float rotationXBoundary, rotationYBoundary;

    public float minVerticalRotation, maxVerticalRotation, rotationSpeed, headRotationSpeed;
    float RotationSpeed => TobiiAPI.IsConnected ? rotationSpeed * 350 : rotationSpeed * 15;

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
        switch (rotationType)
        {
            //Camera and player rotate with the same amount as the player's head
            //Maybe smoother?
            case (RotationType.Continuously):
                transform.rotation = Quaternion.Euler(0, headRotation.eulerAngles.y, 0);
                cam.transform.localRotation = Quaternion.Euler(headRotation.eulerAngles.x, 0, 0);
                break;

            //Character rotates as long as head of the player is rotated to the certain side
            case (RotationType.WithBoundary):
                if (Mathf.Abs(headRotation.y) > rotationYBoundary / 100 * 0.30f)
                {
                    Debug.Log("Rotating Player");
                    float rotationSign = Mathf.Sign(headRotation.y);
                    float rotationXSpeed = Mathf.Pow(Mathf.Abs(headRotation.y) * 10, rotationSpeedIncreaseFactor) * rotationSign * headRotationSpeed * Time.deltaTime;
                    //horizontalRotation += rotationXSpeed;
                    transform.eulerAngles += new Vector3(0, rotationXSpeed, 0); 
                    //transform.localRotation = Quaternion.Euler(0, transform.eulerAngles.y + rotationXSpeed, 0);
                }

                if (Mathf.Abs(headRotation.x) > rotationXBoundary / 100 * 0.30f)
                {
                    Debug.Log("Rotating Cam");
                    float rotationSign = Mathf.Sign(headRotation.x);
                    float rotationYSpeed = Mathf.Pow(Mathf.Abs(headRotation.x) * 10, rotationSpeedIncreaseFactor) * rotationSign * headRotationSpeed * Time.deltaTime;
                    //verticalRotation += rotationYSpeed;
                    //verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
                    //float verticalRotation = cam.transform.eulerAngles.x + rotationYSpeed;
                    //Debug.Log(verticalRotation);
                    //verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
                    //cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
                    cam.transform.eulerAngles += new Vector3(rotationYSpeed, 0, 0);

                    ClampRotation(cam.transform.rotation, new Vector3(90, 0, 0));

                    Debug.Log(cam.transform.eulerAngles.x);

                    //if (cam.transform.eulerAngles.x < minVerticalRotation)
                      //  cam.transform.eulerAngles = new Vector3(minVerticalRotation, 0, 0);

                    //if (cam.transform.eulerAngles.x > maxVerticalRotation)
                       // cam.transform.eulerAngles = new Vector3(maxVerticalRotation, 0, 0);
                }
                break;
        }
    }

    public static Quaternion ClampRotation(Quaternion q, Vector3 bounds)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -bounds.x, bounds.x);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
        angleY = Mathf.Clamp(angleY, -bounds.y, bounds.y);
        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleZ = Mathf.Clamp(angleZ, -bounds.z, bounds.z);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q.normalized;
    }
}