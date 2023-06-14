//(c) copyright by Martin M. Klöckener
using UnityEngine;

[DisallowMultipleComponent]
public class LookAround : FirstPersonModule
{
    public GameObject cam;

    float verticalRotation;

    Vector3 mousePos;

    Quaternion headRotation;
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
        //Camera and player rotate with the same amount as the player's head
        transform.Rotate(Vector3.up, headRotation.eulerAngles.y);
        cam.transform.localRotation = Quaternion.Euler(headRotation.eulerAngles.x, 0, 0);

        // Character rotates as long as head of the player is rotated to the certain side
        if (Mathf.Abs(headRotation.eulerAngles.x) > rotationXBoundary)
        {
            float rotationXSpeed = Mathf.Pow(headRotation.eulerAngles.x, rotationSpeedIncreaseFactor) * Time.deltaTime * 350;
            transform.Rotate(Vector3.up, rotationXSpeed);
        }

        if (Mathf.Abs(headRotation.eulerAngles.y) > rotationYBoundary)
        {
            float rotationYSpeed = Mathf.Pow(headRotation.eulerAngles.y, rotationSpeedIncreaseFactor) * Time.deltaTime * 350;
            cam.transform.localRotation = Quaternion.Euler(rotationYSpeed, 0, 0);
        }
    }
}