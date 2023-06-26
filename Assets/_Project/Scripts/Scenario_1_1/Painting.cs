using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Painting : MonoBehaviour
{
    public UnityEvent OnPaintingTriggerEnter;
    public UnityEvent OnPaintingTriggerExit;
    
    private bool resettingPos;
    private Vector3 startPos;
    private Rigidbody rb;

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

    //this gets called every frame when the painting is inside the trigger. Trigger progress is a value between 0 and 1. Use it e.g. for animating an increasing glow or so.
    public void OnPaintingTriggerStay(float triggerProgressNormalized)
    {
        //do beautiful animations here :))
    }
    
    //this is just for demonstration purposes. Delete it as soon as there is different visual feedback for entering or exiting the trigger zone
    public void EXAMPLE_ScaleUp() => transform.DOScale(0.95f, 0.25f);
    public void EXAMPLE_ScaleDown() => transform.DOScale(0.85f, 0.25f);

}
