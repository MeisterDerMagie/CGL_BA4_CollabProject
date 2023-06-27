using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Shaking : FirstPersonModule
{
    private float latestAddedValue, addedValue;
    private float shakingTime;
    public float ShakingTime => shakingTime;

    private Camera cam;

    private void Awake() => cam = Camera.main;

    public void ExecuteShaking(Quaternion rotation)
    {
        if (!IsEnabled) return;

        transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        cam.transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, 0, 0);

        addedValue = rotation.x + rotation.y + rotation.z;
        Debug.Log(Mathf.Abs(latestAddedValue - addedValue) * 10);
        /*if (Mathf.Abs(latestAddedValue - addedValue) * 10 >= 0.9f)
        {
            shakingTime += Time.deltaTime;
            Debug.Log(shakingTime);
        }
        else
        {
            Debug.Log("Shaking Time Reset");
            shakingTime = 0;
        }
        latestAddedValue = addedValue;*/
    }
}
