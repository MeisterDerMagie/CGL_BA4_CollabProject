using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Shaking : FirstPersonModule
{
    private float latestAddedValue, addedValue;
    private float shakingTime;
    public float ShakingTime => shakingTime;

    public void ExecuteShaking(Quaternion headRotation)
    {
        if (!IsEnabled) return;

        addedValue = headRotation.x + headRotation.y + headRotation.z;
        //Debug.Log(Mathf.Abs(latestAddedValue - addedValue) * 100);
        if (Mathf.Abs(latestAddedValue - addedValue) * 10 >= 0.9f)
        {
            shakingTime += Time.deltaTime;
            Debug.Log(shakingTime);
        }
        else
        {
            Debug.Log("Shaking Time Reset");
            shakingTime = 0;
        }
        latestAddedValue = addedValue;
    }
}
