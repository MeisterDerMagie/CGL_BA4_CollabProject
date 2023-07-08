using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Shaking : FirstPersonModule
{
    private float latestAddedValue, addedValue;
    private float shakingTime;
    public float ShakingTime => shakingTime;

    private int stopped;

    private Camera cam;

    private void Awake() => cam = Camera.main;

    public void ExecuteShaking(Quaternion rotation)
    {
        if (!IsEnabled) return;

        //Apply head rotation to player and camera
        transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        cam.transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, 0, 0);

        //Check if the rotation changed compared to the last frame
        addedValue = rotation.x + rotation.y + rotation.z;
        if (Mathf.Abs(latestAddedValue - addedValue) * 10 >= 0.9f)
        {
            //Add time to timer
            shakingTime += Time.deltaTime;

            //Reset stopped couter
            stopped = 0;
        }
        else
        {
            //Reset timer if player stopped shaking for 4 frames
            if (stopped == 4)
            {
                shakingTime = 0;
            }
            //Add to counter
            else stopped++;
        }
        //Update lates rotation value
        latestAddedValue = addedValue;
    }
}
