//(c) copyright by Martin M. Klöckener
using UnityEngine;

public class LookAround : FirstPersonModule
{
    public GameObject cam;

    float verticalRotation, edgeSize;

    public float minVerticalRotation, maxVerticalRotation, rotationSpeed;
    [Range(0, 50)]
    public float screenBoundaryPercentage;
    [Range(0, 5)]
    public float rotationSpeedIncreaseFactor;

    public void ExecuteLookAround(Vector3 mousePos)
    {
        if (!IsEnabled)
        {
            //Debug.Log("Look around is not enabled.");
            return;
        }

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
            float currentXRotationSpeed = mouseXCenterDistance * Mathf.Pow(rotationSpeed, rotationSpeedIncreaseFactor) * mouseXSide;
            transform.Rotate(Vector3.up, currentXRotationSpeed);
        }

        // Rotation on Y-Axis
        if (mouseYCenterDistance > 0)
        {
            float currentYRotationSpeed = mouseYCenterDistance * Mathf.Pow(rotationSpeed, rotationSpeedIncreaseFactor) * mouseYSide;
            verticalRotation -= currentYRotationSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        //Debug.Log($"mouse Pos X: {mousePos.x}");
        //Debug.Log($"mouse Pos Y: {mousePos.y}");
    }
}