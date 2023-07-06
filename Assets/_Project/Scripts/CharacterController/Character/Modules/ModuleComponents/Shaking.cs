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

        transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        cam.transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, 0, 0);

        addedValue = rotation.x + rotation.y + rotation.z;
        if (Mathf.Abs(latestAddedValue - addedValue) * 10 >= 0.9f)
        {
            //Debug.Log(Mathf.Abs(latestAddedValue - addedValue) * 10);
            shakingTime += Time.deltaTime;
            Debug.Log(shakingTime);

            stopped = 0;
        }
        else
        {
            //Debug.Log(Mathf.Abs(latestAddedValue - addedValue) * 10);
            //Debug.Log("Shaking Time Reset");
            //StartCoroutine(CheckShaking());
            //shakingTime = 0;
            if (stopped == 4)
            {
                shakingTime = 0;
                Debug.Log("Reset");
            }
            else stopped++;
        }
        latestAddedValue = addedValue;
    }

    IEnumerator CheckShaking()
    {
        yield return new WaitForSeconds(0.3f);
        if (Mathf.Abs(latestAddedValue - addedValue) * 10 >= 0.03f) shakingTime += Time.deltaTime;
        else
        {
            shakingTime = 0;
            Debug.Log("Reset");
        }
    }
}
