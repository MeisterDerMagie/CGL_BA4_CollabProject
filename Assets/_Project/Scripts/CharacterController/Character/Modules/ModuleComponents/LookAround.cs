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

        // Calculation for X Rotation
        float mouseXPercentage = Mathf.Clamp(mousePos.x / Screen.width, 0f, 1f);
        float mouseXSide = Mathf.Sign(mouseXPercentage - 0.5f);
        float mouseXCenterDistance = (mouseXPercentage - 0.5f) * mouseXSide;
        mouseXCenterDistance = mouseXCenterDistance - (0.5f - screenBoundaryPercentage / 100f);
        
        // Calculation for Y Rotation
        float mouseYPercentage = Mathf.Clamp(mousePos.y / Screen.height, 0f, 1f);
        float mouseYSide = Mathf.Sign(mouseYPercentage - 0.5f);
        float mouseYCenterDistance = (mouseYPercentage - 0.5f) * mouseYSide;
        mouseYCenterDistance = mouseYCenterDistance - (0.5f - screenBoundaryPercentage / 100f);
        
        // Don't move camera when the mouse is outside the screen
        if (mousePos.x < 0f || mousePos.x > Screen.width || mousePos.y < 0f || mousePos.y > Screen.height)
        {
            mouseXCenterDistance = 0f;
            mouseYCenterDistance = 0f;
        }

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

        //move player/camera here
        /*verticalRotation -= amount.y;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up, amount.x);

        Debug.Log($"move player on x axis for: {amount.x}");
        Debug.Log($"move player on y axis for: {amount.y}");*/
    }
}