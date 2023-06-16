using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [SerializeField]
    float secondsInMiddle;

    float timeInMiddle;

    bool resettingPos;

    Vector3 startPos;

    Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Start() => startPos = transform.position;

    public void Move(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void ResetPosition()
    {
        rb.velocity = Vector3.zero;

        if (resettingPos == true) return;

        StartCoroutine(ResetPos());
        resettingPos = false;
    }

    IEnumerator ResetPos()
    {
        resettingPos = true;
        float startTime = Time.time;
        while (Time.time - startTime <= 0.6f)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.time - startTime);
            yield return 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        if (other.tag != "Middle") return;

        timeInMiddle += Time.deltaTime;
        if (timeInMiddle >= secondsInMiddle)
        {
            Debug.Log("Scene Transition");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.tag != "Middle") return;

        timeInMiddle = 0f;
    }
}
