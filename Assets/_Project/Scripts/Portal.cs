//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Collider))]
public class Portal : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onEnterPortal;

    [SerializeField][Range(-360f, 360f)]
    float minRotationSpeed, maxRotationSpeed;

    Animation anim;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _onEnterPortal.Invoke();
    }

    private void Start()
    {
        anim = GetComponent<Animation>();

        foreach(Transform child in transform)
        {
            child.GetComponent<PortalRotation>().rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }
    }

    public void PlayAnimation()
    {
        Debug.Log("Anim Play");
        anim.Play("PortalAssamble");
    }

    public void StopAnimation()
    {
        Debug.Log("Anim Stop");
        anim.Stop();
    }
}