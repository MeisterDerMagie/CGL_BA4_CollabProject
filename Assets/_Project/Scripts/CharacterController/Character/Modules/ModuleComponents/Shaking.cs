using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Shaking : FirstPersonModule
{
    private float latestAddedValue, addedValue;
    private float shakingTime;
    public float ShakingTime => shakingTime;
    private float timeToShake;

    private int stopped;

    private Camera cam;

    public ShakingFeedback feedback;

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

            //Making the image gradually darker (less transparent)
            feedback.FadeIn(shakingTime);

            //Reset Counter
            stopped = 0;
        }
        else
        {
            StartCoroutine(CheckShaking());
        }
        //Update lates rotation value
        latestAddedValue = addedValue;
    }

    IEnumerator CheckShaking()
    {
        yield return new WaitForSeconds(0.01f);
        if (Mathf.Abs(latestAddedValue - addedValue) * 10 >= 0.9f)
        {
            shakingTime += Time.deltaTime;
            feedback.FadeIn(shakingTime);
            yield break;
        }
        else
        {
            stopped++;
            if (stopped == 4)
            {
                shakingTime = 0;
                feedback.FadeOut();
            }
        }
    }
}
