//(c) copyright by Martin M. Klöckener
using UnityEngine;

public class LookAround : FirstPersonModule
{
    public GameObject cam;

    float verticalRotation, edgeSize;
    public float minVerticalRotation, maxVerticalRotation;

    /*public void ExecuteLookAround(Vector3 mousePos)
    {
        if (!IsEnabled)
        {
            Debug.Log("Look around is not enabled.");
            return;
        }

        if (mousePos.x >)

        //move player/camera here
        verticalRotation -= amount.y;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up, amount.x);

        Debug.Log($"move player on x axis for: {amount.x}");
        Debug.Log($"move player on y axis for: {amount.y}");
    }*/
}