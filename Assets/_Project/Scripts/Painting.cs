using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField]
    float secondsInMiddle;

    float timeInMiddle;

    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        if (other.tag != "Middle") return;

        timeInMiddle += Time.deltaTime;
        if (timeInMiddle >= secondsInMiddle)
        {
            Debug.Log("Scene Transition");
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.tag != "Middle") return;

        timeInMiddle = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }
}
