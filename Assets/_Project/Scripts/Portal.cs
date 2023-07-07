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

    Animator animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _onEnterPortal.Invoke();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Focus (bool focus) => animator.SetBool("Focus", focus);
}