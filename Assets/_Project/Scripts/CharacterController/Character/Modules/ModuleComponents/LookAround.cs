//(c) copyright by Martin M. Klöckener
using UnityEngine;

public class LookAround : FirstPersonModule
{
    float verticalRotation;
    public float minVerticalRotation, maxVerticalRotation;
    public GameObject cam;

    public void ExecuteLookAround(Vector2 amount)
    {
        if (!IsEnabled)
        {
            Debug.Log("Look around is not enabled.");
            return;
        }

        //move player here
        verticalRotation -= amount.y;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up, amount.x);

        Debug.Log($"move player on x axis for: {amount.x}");
        Debug.Log($"move player on y axis for: {amount.y}");
    }
}